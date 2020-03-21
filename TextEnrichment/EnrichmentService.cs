using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TextEnrichment.Text;
using TextEnrichment.Word;


namespace TextEnrichment
{
    public class EnrichmentService : BackgroundService
    {
        private readonly ISentencer sentencer;
        private readonly IDocumentReader documentReader;
        private readonly IDocumentWriter documentWriter;

        private readonly IHostApplicationLifetime applicationLifetime;

        public EnrichmentService(ISentencer sentencer, IDocumentReader documentReader, IDocumentWriter documentWriter,
            IHostApplicationLifetime applicationLifetime)
        {
            this.sentencer = sentencer;
            this.documentReader = documentReader;
            this.documentWriter = documentWriter;
            this.applicationLifetime = applicationLifetime;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            foreach (var paragraph in documentReader.ReadParagraphs("textBefore.docx"))
            {
                var sentences = sentencer.GetSentencesFromParagraph(paragraph);

                foreach (var (sentence, tag) in sentences)
                {
                    documentWriter.InsertSentence(sentence, tag ?? string.Empty);
                }

                documentWriter.InsertEmptyLine();
            }

            documentWriter.Save("textAfter.docx");

            applicationLifetime.StopApplication();

            return Task.CompletedTask;
        }
    }
}
