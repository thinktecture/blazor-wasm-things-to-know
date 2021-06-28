using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace BlazorWasmVideo.Client
{
    public static class ClientStartup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<VideoServiceJSInterop>();
            services.AddScoped<VideoServiceUnmarshaledJSInterop>();

            services.AddMudServices();
        }
    }
}
