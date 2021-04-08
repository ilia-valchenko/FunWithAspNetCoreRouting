using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FunWithAspNetCoreRouting
{
    public class Startup_RouteConstraints
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

            var myRouteHandler = new RouteHandler(Handle);
            var routeBuilder = new RouteBuilder(app, myRouteHandler);

            // 1. For route restriction we use an overloaded version of MapRoute(string name, string template, object defaults, object constraints)

            //routeBuilder.MapRoute("default",
            //    "{controller}/{action}/{id?}",
            //    null,
            //    new { action = "index" });

            // 2. Regular expression

            //routeBuilder.MapRoute("default",
            //    "{controller}/{action}/{id?}",
            //    null,
            //    new { controller = "^H.*" });

            //routeBuilder.MapRoute("default",
            //    "{controller}/{action}/{id?}",
            //    null,
            //    new { controller = "^H.*", id = @"\d{2}" });

            // 3. Type restriction

            routeBuilder.MapRoute("default",
                "{controller}/{action}/{id?}",
                null,
                new { id = new BoolRouteConstraint() });

            app.UseRouter(routeBuilder.Build());

            // Default
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Default page");
            });
        }

        private async Task Handle(HttpContext context)
        {
            await context.Response.WriteAsync("The route is chosen.");
        }
    }
}