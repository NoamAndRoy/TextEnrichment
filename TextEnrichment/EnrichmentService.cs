using LexicalAnalyzer;
using Microsoft.Extensions.Hosting;
using Novacode;
using System;
using System.Collections.Generic;
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
            var document = DocX.Load(@"C:\Users\Chapnik\Source\Repos\TextEnrichment\example.docx");

            var tokens = lexer.GetTokens(document.Text).ToList();

            var sentences = GetSentences(tokens);

            using var fixedDocument = DocX.Create(@"C:\Users\Chapnik\Source\Repos\TextEnrichment\Fixed.docx");
            fixedDocument.SetDirection(Direction.LeftToRight);

            foreach(var sentence in sentences)
            {
                var sentenceAsText = SentenceToText(sentence);
                Console.WriteLine(sentenceAsText);
                fixedDocument.InsertParagraph(SentenceToText(sentence));
            }

            fixedDocument.Save();

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

        private string SentenceToText(List<Token<eTokenType>> sentence)
        {
            var builder = new StringBuilder(sentence[0].Value.ToString());
            var lastToken = sentence[0];

            foreach(var token in sentence.Skip(1))
            {
                builder.Append(token.TokenType switch
                {
                    eTokenType.Punctuation => token.Value.ToString(),
                    _ when lastToken.TokenType != eTokenType.Punctuation => $" {token.Value.ToString()}",
                    _ => token.Value.ToString()
                });

                lastToken = token;
            }
            return builder.ToString();
        }
    }
}
