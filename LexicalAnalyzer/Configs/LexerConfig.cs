using System;

namespace LexicalAnalyzer.Configs
{
    public class LexerConfig<TEnum> where TEnum : Enum
    {
        public TokenDefinition<TEnum>[] TokensDefinitions { get; set; }
    }
}
