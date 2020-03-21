using System;
using LexicalAnalyzer.ExtensionMethods;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using TextEnrichment.Configs;
using TextEnrichment.Tags;
using TextEnrichment.Text;
using TextEnrichment.Word;
using System.Windows.Forms;

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
            var sentencer = host.Services.GetService<ISentencer>();
            var documentReader = host.Services.GetService<IDocumentReader>();
            var documentWriter = host.Services.GetService<IDocumentWriter>();
            using var mainWindow = new MainForm(sentencer, documentReader, documentWriter);

            Application.Run(mainWindow);
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
                .AddTransient<ISentencer, Sentencer>();
        }
    }
}
