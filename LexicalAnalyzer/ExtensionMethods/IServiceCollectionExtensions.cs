using LexicalAnalyzer.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LexicalAnalyzer.ExtensionMethods
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddLexer<TTokenTypeEnum>(this IServiceCollection services,
            params TokenDefinition<TTokenTypeEnum>[] tokenDefinitions) where TTokenTypeEnum : Enum
        { 
            services.AddTransient<ILexer<TTokenTypeEnum>, Lexer<TTokenTypeEnum>>(provider =>
            {
                var lexer = new Lexer<TTokenTypeEnum>();

                foreach (var tokenDefinition in tokenDefinitions)
                {
                    lexer.AddTokenDefinition(tokenDefinition.TokenType, tokenDefinition.Pattern);
                }

                return lexer;
            });

            return services;
        }

        public static IServiceCollection AddLexerFromConfiguration<TTokenTypeEnum>(this IServiceCollection services,
            IConfiguration configuration) where TTokenTypeEnum : Enum
        {
            return services.Configure<LexerConfig<TTokenTypeEnum>>(configuration.GetSection("Lexer"))
                .AddLexer<TTokenTypeEnum>();
        }
    }
}
