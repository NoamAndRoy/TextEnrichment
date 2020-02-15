using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace TextEnrichment
{
    public static class Program
    {
        public static async Task Main()
        {
            var builder = new HostBuilder();

            await builder.RunConsoleAsync().ConfigureAwait(false);
        }
    }
}
