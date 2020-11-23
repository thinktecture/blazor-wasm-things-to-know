using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using System;
using BlazorWasmPrerendering.Server.Model;
using Microsoft.EntityFrameworkCore;
using ProtoBuf.Grpc.Server;
using BlazorWasmPrerendering.Server.GrpcServices;
using BlazorWasmPrerendering.Client;

namespace BlazorWasmPrerendering.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ClientStartup.ConfigureServices(services, GetBaseUrl(), true);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDbContext<ConferencesDbContext>(
                options => options.UseInMemoryDatabase(databaseName: "BlazorWasmPrerendering"));

            services.AddCodeFirstGrpc(config => { config.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Optimal; });

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseGrpcWeb();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<ConferencesService>().EnableGrpcWeb();
                endpoints.MapGrpcService<TimeService>().EnableGrpcWeb();

                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private string GetBaseUrl()
        {
            string baseUrl;
            var baseUrls = Configuration[WebHostDefaults.ServerUrlsKey];

            if (baseUrls.Contains(";"))
            {
                baseUrl = baseUrls.Split(";")[0];
            }
            else
            {
                baseUrl = baseUrls;
            }

            return baseUrl;
        }
    }
}
