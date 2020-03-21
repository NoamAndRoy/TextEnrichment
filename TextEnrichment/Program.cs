using LexicalAnalyzer.ExtensionMethods;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using TextEnrichment.Configs;
using TextEnrichment.Tags;
using TextEnrichment.Text;
using TextEnrichment.Word;

namespace TextEnrichment
{
    public static class Program
    {
        public static async Task Main()
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration(ConfigureConfiguration)
                .ConfigureServices(ConfigureServices);

            await builder.RunConsoleAsync().ConfigureAwait(false);
        }

        private static void ConfigureConfiguration(HostBuilderContext context, IConfigurationBuilder config)
        {
            config
                .AddJsonFile("appsettings.json", false, false)
                .Build();
        }

        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddOptions()
                .Configure<DataFilesConfig>(context.Configuration.GetSection("DataFiles"));

            services.AddLexer<eTokenType>()
                .AddTransient<ITagsLoader, TagsCSVLoader>()
                .AddTransient<IDocumentReader, DocumentReader>()
                .AddTransient<IDocumentWriter, DocumentWriter>()
                .AddTransient<ISentencer, Sentencer>()
                .AddHostedService<EnrichmentService>();
        }
    }
}
