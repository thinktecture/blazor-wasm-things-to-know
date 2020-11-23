using System.Threading.Tasks;
using ConfTool.ClientModules.Conferences;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace ConfTool.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddConferencesModule(builder.Configuration);
            
            await builder.Build().RunAsync();
        }
    }
}
