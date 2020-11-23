using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorWasmPrerendering.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            ClientStartup.ConfigureServices(builder.Services, builder.HostEnvironment.BaseAddress, false);

            await builder.Build().RunAsync();
        }
    }
}
