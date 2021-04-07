using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FunWithAspNetCoreRouting
{
    public class Startup4
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

            // Define route handlers.
            var myRouteHandlerHome = new RouteHandler(HandleHome);
            var myRouteHandlerHomeCourses = new RouteHandler(HandleHomeCourses);

            // Create routes.
            var routeBuilderHome = new RouteBuilder(app, myRouteHandlerHome);
            var routeBuilderHomeCourses = new RouteBuilder(app, myRouteHandlerHomeCourses);

            // Routes must match the specified static templates.
            routeBuilderHome.MapRoute("default", "home");
            routeBuilderHomeCourses.MapRoute("default", "home/courses");

            // Build routes.
            app.UseRouter(routeBuilderHome.Build());
            app.UseRouter(routeBuilderHomeCourses.Build());

            // Default
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Default page");
            });
        }

        // The route handler itself.
        private async Task HandleHome(HttpContext context)
        {
            await context.Response.WriteAsync("Home page");
        }

        private async Task HandleHomeCourses(HttpContext context)
        {
            await context.Response.WriteAsync("Page with courses");
        }
    }
}