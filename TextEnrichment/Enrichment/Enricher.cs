using System;
using TextEnrichment.Text;
using TextEnrichment.WordDocument;

namespace TextEnrichment.Enrichment
{
    public class Enricher : IEnricher
    {
        private readonly ISentencer sentencer;
        private readonly IDocumentReader documentReader;
        private readonly IDocumentWriter documentWriter;

        public Enricher(ISentencer sentencer, IDocumentReader documentReader, IDocumentWriter documentWriter)
        {
            this.sentencer = sentencer;
            this.documentReader = documentReader;
            this.documentWriter = documentWriter;
        }

        public void Enrich(string filePath, string enrichedFilePath)
        {
            foreach (var paragraph in documentReader.ReadParagraphs(filePath))
            {
                var sentences = sentencer.GetSentences(paragraph);

                foreach (var (sentence, tag) in sentences)
                {
                    documentWriter.InsertSentence(sentence, tag ?? string.Empty);
                }

                documentWriter.InsertEmptyLine();
            }

            documentWriter.Save(enrichedFilePath);
        }
    }
}
