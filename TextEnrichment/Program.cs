using LexicalAnalyzer.ExtensionMethods;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Windows.Forms;
using TextEnrichment.Configs;
using TextEnrichment.Enrichment;
using TextEnrichment.Tags;
using TextEnrichment.Text;
using TextEnrichment.WordDocument;

namespace TextEnrichment
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var host = new HostBuilder()
                .ConfigureAppConfiguration(ConfigureConfiguration)
                .ConfigureServices(ConfigureServices)
                .Build();

            RunMainForm(host);
        }

        private static void RunMainForm(IHost host)
        {
            var enricher = host.Services.GetService<IEnricher>();
            using var mainWindow = new MainForm(enricher);

            Application.Run(mainWindow);
        }

        private static void ConfigureConfiguration(HostBuilderContext context, IConfigurationBuilder config)
        {
            config
                .SetBasePath(Directory.GetCurrentDirectory())
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
                .AddTransient<IEnricher, Enricher>();
        }
    }
}
