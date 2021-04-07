using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FunWithAspNetCoreRouting
{
    public class Startup3
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

            // The first parameter is a template. It will be used by URL Matching mechanism.
            // The second parameter is Handler. It's an instance of the RequestDelefate.
            routeBuilder.MapRoute("{somethingLikeController}",
                async context => {
                    await context.Response.WriteAsync("{somethingLikeController} template used");
                });

            routeBuilder.MapRoute("{somethingLikeController}/{somethingLikeAction}",
                async context => {
                    await context.Response.WriteAsync("{somethingLikeController}/{somethingLikeAction} template used");
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