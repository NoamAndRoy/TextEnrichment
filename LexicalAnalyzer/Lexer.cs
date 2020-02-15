using LexicalAnalyzer.Configs;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LexicalAnalyzer
{
    public class Lexer<TEnum> : ILexer<TEnum> where TEnum : Enum
    {
        private readonly List<TokenDefinition<TEnum>> tokensDefinitions;

        public Lexer(IOptions<LexerConfig<TEnum>> config)
        {
            tokensDefinitions = new List<TokenDefinition<TEnum>>();

            if (config.Value.TokensDefinitions != null)
            {
                tokensDefinitions.AddRange(config.Value.TokensDefinitions);
            }
        }

        public Lexer()
        {
            tokensDefinitions = new List<TokenDefinition<TEnum>>();
        }

        public IEnumerable<Token<TEnum>> GetTokens(string text)
        {
            while (!string.IsNullOrWhiteSpace(text))
            {
                var token = FindToken(text);

                if (token != null)
                {
                    yield return token;
                }

                text = text.Substring(token?.Value.Length ?? 1);
            }
        }

        public void AddTokenDefinition(TEnum tokenType, string pattern)
        {
            tokensDefinitions.Add(new TokenDefinition<TEnum>(tokenType, pattern));
        }

        private Token<TEnum>? FindToken(string text)
        {
            return tokensDefinitions
                .Select(tokenDefinition => tokenDefinition.Match(text))
                .FirstOrDefault(token => token != null);
        }
    }
}
