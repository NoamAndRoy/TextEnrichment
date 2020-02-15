using System;
using System.Text.RegularExpressions;

namespace LexicalAnalyzer
{
    public class TokenDefinition<TEnum> where TEnum : Enum
    {
        private Regex regex = new Regex(string.Empty);

        public TEnum TokenType { get; internal set; }

        public string Pattern
        {
            get => regex.ToString();
            internal set => regex = new Regex(value);
        }

        public TokenDefinition(TEnum tokenType, string pattern)
        {
            TokenType = tokenType;
            Pattern = pattern;
        }

        public Token<TEnum>? Match(string text)
        {
            var match = regex.Match(text);

            return match.Success ? new Token<TEnum>(TokenType, match.Value) : null;
        }
    }
}
