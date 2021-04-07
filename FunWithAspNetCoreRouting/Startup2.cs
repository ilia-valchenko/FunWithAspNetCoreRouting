using System.Collections.Specialized;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FunWithAspNetCoreRouting
{
    public class Startup2
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

            routeBuilder.MapRoute("home",
                async context =>
                {
                    await context.Response.WriteAsync("Home page");
                });

            routeBuilder.MapRoute("home/courses", async context =>
            {
                StringCollection courses = new StringCollection()
                    {
                        "C# Starter",
                        "C# Essential",
                        "C# Professional",
                        "C# Patterns of Design"
                    };

                await context.Response.WriteAsync("Here is the list of available courses:");

                foreach (var course in courses)
                {
                    await context.Response.WriteAsync($"<br>{course}</br>");
                }
            });

            app.UseRouter(routeBuilder.Build());

            app.UseRouting();
        }
    }
}