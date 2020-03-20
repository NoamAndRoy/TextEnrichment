using Aspose.Words;
using LexicalAnalyzer;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace TextEnrichment
{
    public class EnrichmentService : BackgroundService
    {
        private readonly ILexer<eTokenType> lexer;
        private readonly IHostApplicationLifetime applicationLifetime;

        public EnrichmentService(ILexer<eTokenType> lexer, IHostApplicationLifetime applicationLifetime)
        {
            this.lexer = lexer;
            this.applicationLifetime = applicationLifetime;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var document = new Document("textBefore.docx");
            var fixedDocument = new Document();
            var documentBuilder = new DocumentBuilder(fixedDocument);

            var paragraphs = document.GetChildNodes(NodeType.Paragraph, true);

            var font = documentBuilder.Font;
            font.Name = "Calibri";
            font.Size = 12;

            documentBuilder.StartTable();

            foreach (var paragraph in paragraphs)
            {
                var tokens = lexer.GetTokens(paragraph.GetText()).ToList();
                var sentences = GetSentences(tokens);

                foreach (var sentenceAsText in sentences.Select(sentence => SentenceToText(sentence)))
                {
                    documentBuilder.InsertCell();
                    documentBuilder.Writeln("some title");

                    documentBuilder.InsertCell();
                    documentBuilder.Writeln(sentenceAsText);

                    documentBuilder.EndRow();
                }

                documentBuilder.InsertCell();
                documentBuilder.InsertCell();
                var row = documentBuilder.EndRow();
                row.RowFormat.Height = 20;
            }

            var table = documentBuilder.EndTable();
            table.ClearBorders();
            table.SetBorder(BorderType.Vertical, LineStyle.Single, 1, Color.Black, true);

            fixedDocument.Save("textAfter.docx");

            applicationLifetime.StopApplication();

            return Task.CompletedTask;
        }


        private List<List<Token<eTokenType>>> GetSentences(List<Token<eTokenType>> tokens)
        {
            var sentences = new List<List<Token<eTokenType>>>();
            var indexOfSentenceEnd = 0;

            while (indexOfSentenceEnd != -1)
            {
                indexOfSentenceEnd = FindSentenceEnd(tokens);

                if (indexOfSentenceEnd == -1)
                {
                    sentences.Add(tokens);
                    break;
                }

                sentences.Add(tokens.GetRange(0, indexOfSentenceEnd + 1));

                tokens = tokens.Skip(indexOfSentenceEnd + 1).ToList();
            }

            return sentences;
        }

        private int FindSentenceEnd(List<Token<eTokenType>> tokens)
        {
            var index = tokens.FindIndex(token => token.TokenType == eTokenType.Punctuation && token.Value.ToString() == ".");
            if (tokens.Count > index + 1
                && tokens[index + 1].TokenType != eTokenType.Punctuation
                && char.IsUpper(tokens[index + 1].Value.ToString()[0]))
            {
                return index;
            }

            return -1;
        }

        private string SentenceToText(List<Token<eTokenType>> words)
        {
            if (words.Count == 0)
            {
                return string.Empty;
            }

            var builder = new StringBuilder(AddNumber(words[0].Value.ToString()) + words[0].Value);
            var lastToken = words[0];

            foreach (var token in words.Skip(1))
            {
                builder.Append(token.TokenType switch
                {
                    eTokenType.Punctuation when token.Value.ToString() == "(" || token.Value.ToString() == "-" => $" {token.Value.ToString()}",
                    eTokenType.Punctuation => token.Value.ToString(),
                    _ when lastToken.Value.ToString() != "(" => $" {token.Value.ToString()}",
                    _ => token.Value.ToString()
                });

                lastToken = token;
            }

            return builder.ToString();
        }

        private string AddNumber(string word) =>
            word.ToLower() switch
            {
                "first" => "1. ",
                "second" => "2. ",
                "third" => "3. ",
                "fourth" => "4. ",
                "fifth" => "5. ",
                "sixth" => "6. ",
                "seventh" => "7. ",
                "eighth" => "8. ",
                "ninth" => "9. ",
                _ => ""
            };
    }
}
