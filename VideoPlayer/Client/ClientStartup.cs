using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;

namespace BlazorWasmVideo.Client
{
    public static class ClientStartup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<VideoService>();

            services.AddMudBlazorDialog();
            services.AddMudBlazorSnackbar();
            services.AddMudBlazorResizeListener();
        }
    }
}
