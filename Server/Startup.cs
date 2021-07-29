using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProtoBuf.Grpc.Server;
using Shared.Contract;
using Shared.Implementation;

namespace Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ITestService, TestService>();
            services.AddGrpc();
            services.AddCodeFirstGrpc();
            services.AddCodeFirstGrpcReflection();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<ITestService>();

                endpoints.MapCodeFirstGrpcReflectionService();

                endpoints.MapGet("/",
                    async context =>
                    {
                        await context.Response.WriteAsync("").ConfigureAwait(false);
                    });
            });
        }
    }
}
