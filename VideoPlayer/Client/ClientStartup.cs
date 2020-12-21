using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;

namespace BlazorWasmPrerendering.Client
{
    public static class ClientStartup
    {
        public static void ConfigureServices(IServiceCollection services, bool isServer)
        {
            services.AddMudBlazorDialog();
            services.AddMudBlazorSnackbar();
            services.AddMudBlazorResizeListener();
        }
    }
}
