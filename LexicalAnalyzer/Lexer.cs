using LexicalAnalyzer.Configs;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LexicalAnalyzer
{
    public class Lexer<TEnum> : ILexer<TEnum> where TEnum : Enum
    {
        private readonly IOptions<LexerConfig<TEnum>> config;

        public Lexer(IOptions<LexerConfig<TEnum>> config)
        {
            this.config = config;
        }

        public IEnumerable<Token<TEnum>> GetTokens(string text)
        {
            var memoryText = text.AsMemory();

            while (!memoryText.IsEmpty)
            {
                var token = FindToken(memoryText);

                if (token != null)
                {
                    yield return token;
                }

                memoryText = memoryText.Slice(token?.Value.Length ?? 1);
            }
        }

        private Token<TEnum>? FindToken(ReadOnlyMemory<char> text)
        {
            return config.Value.TokensDefinitions
                .Select(tokenDefinition => tokenDefinition.Match(text))
                .FirstOrDefault(token => token != null);
        }
    }
}
