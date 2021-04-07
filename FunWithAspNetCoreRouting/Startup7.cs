using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FunWithAspNetCoreRouting
{
    public class Startup7
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var routeBuilder = new RouteBuilder(app);

            // NOTE: By using catchall we assume that we can have an infinite number of segments.
            routeBuilder.MapRoute("{controller}/{action}/{id}/{*catchall}",
                async context =>
                {
                    await context.Response.WriteAsync("{controller}/{action}/{id}/{*catchall} template used");
                });

            app.UseRouter(routeBuilder.Build());

            // Default
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Default page");
            });
        }
    }
}