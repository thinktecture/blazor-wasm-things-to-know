using Microsoft.Extensions.DependencyInjection;

namespace BlazorLibs.Webcam
{
    public static class WebcamServiceCollectionExtensions
    {
        public static IServiceCollection AddWebcam(this IServiceCollection services)
        {
            services.AddSingleton<IWebcamService, WebcamService>();

            return services;
        }
    }
}
