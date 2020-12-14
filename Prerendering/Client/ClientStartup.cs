using System;
using System.Net.Http;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorWasmPrerendering.Client
{
    public static class ClientStartup
    {
        public static void ConfigureServices(IServiceCollection services, bool isServer)
        {
            services.AddScoped<GrpcChannel>(serviceProvider =>
            {
                var navigationManager = serviceProvider.GetRequiredService<NavigationManager>();
                var address = navigationManager?.BaseUri;
                var grpcWebHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());

                if (isServer)
                {
                    grpcWebHandler.HttpVersion = new Version(1, 1);
                }

                var channel = GrpcChannel.ForAddress(address, new GrpcChannelOptions { HttpHandler = grpcWebHandler });

                return channel;
            });
        }
    }
}
