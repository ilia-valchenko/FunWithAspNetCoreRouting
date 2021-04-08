using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FunWithAspNetCoreRouting
{
    public class Startup_RoutingInformation
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

            routeBuilder.MapRoute("home/{somethingLikeAction}/{somethingLikeid}",
                async context =>
                {
                    RouteData data = context.GetRouteData();

                    foreach (KeyValuePair<string, object> item in data.Values)
                    {
                        await context.Response.WriteAsync($"<br>{item.ToString()}</br>");
                    }
                });

            routeBuilder.MapRoute("{somethingLikeController}/{somethingLikeAction}/{somethingLikeid}",
                async context =>
                {
                    string controller = context.GetRouteValue("somethingLikeController").ToString();
                    string action = context.GetRouteValue("somethingLikeAction").ToString();
                    string id = context.GetRouteValue("somethingLikeId").ToString();

                    await context.Response.WriteAsync($"<br>{controller}</br>");
                    await context.Response.WriteAsync($"<br>{action}</br>");
                    await context.Response.WriteAsync($"<br>{id}<br>");
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