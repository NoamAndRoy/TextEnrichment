using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using LexicalAnalyzer;
using Novacode;


namespace TextEnrichment
{
    public class EnrichmentService : BackgroundService
    {
        private readonly ILexer<eTokenType> lexer;
        public EnrichmentService(ILexer<eTokenType> lexer)
        {
            this.lexer = lexer;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var document = DocX.Load(@"D:\נועם\Elad Segev\TextEnrichment\example.docx");

            var tokens = lexer.GetTokens(document.Text).ToList();

            var sentences = GetSentences(tokens);

            return Task.CompletedTask;
        }


        private IEnumerable<IEnumerable<Token<eTokenType>>> GetSentences(List<Token<eTokenType>> tokens)
        {
            var sentences = new List<IEnumerable<Token<eTokenType>>>();
            var indexOfSentenceEnd = 0;

            while(indexOfSentenceEnd != -1)
            {
                indexOfSentenceEnd = tokens.FindIndex(token => token.TokenType == eTokenType.Period);

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
    }
}
