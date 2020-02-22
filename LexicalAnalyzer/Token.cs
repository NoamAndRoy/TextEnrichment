using System;

namespace LexicalAnalyzer
{
    public class Token<TEnum> where TEnum : Enum
    {
        public TEnum TokenType { get; }
        public string Value { get; }

        internal Token(TEnum tokenType, string value)
        {
            TokenType = tokenType;
            Value = value;
        }

        public override bool Equals(object? other)
        {
            return other is Token<TEnum> otherToken &&
                   (TokenType.Equals(otherToken.TokenType) && Value == otherToken.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TokenType, Value);
        }
    }
}
