using System;
using System.Net.Http;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;

using Microsoft.Extensions.DependencyInjection;

namespace BlazorWasmPrerendering.Client
{
    public static class ClientStartup
    {
        public static void ConfigureServices(IServiceCollection services, string baseUrl, bool isServer)
        {
            services.AddScoped<GrpcChannel>(services =>
            {
                var grpcWebHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());

                if(isServer)
                {
                    grpcWebHandler.HttpVersion = new Version(1, 1);
                }

                var channel = GrpcChannel.ForAddress(baseUrl, new GrpcChannelOptions { HttpHandler = grpcWebHandler });

                return channel;
            });
        }
    }
}
