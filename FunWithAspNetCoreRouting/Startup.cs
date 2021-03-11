using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FunWithAspNetCoreRouting
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        // ASP .NET Core has built-in dependency injection functionality.
        // It has built-in IoC container.
        // If you want more features such as auto-registration, scanning, interceptors, or decorators
        // then you may replace built-in IoC container with a third party container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // Adds routing middleware. It's a part of .AddMvc() in .NET Core 3.
            // So you don't need to add it manually.
            //services.AddRouting();

            Services.Infrastructure.Bootstrapper.ConfigureServices(services);
        }

        // This method gets called by the runtime.
        // Use this method to configure the HTTP request pipeline.
        // Don't forget that you can pass the ILoggerFactory.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Q: What are the differences between app.UseRouting() and app.UseEndPoints()?
            // A: UseRouting matches request to an endpoint. UseEndpoints executes the matched endpoint.

            // It decouples the route matching and resolution functionality from the endpoint executing functionality,
            // which until now was all bundled in with the MVC middleware.

            // This makes the ASP.NET Core framework more flexible and allows other middlewares to act between UseRouting
            // and UseEndpoints. That allows those middlewares to utilize the information from endpoint routing, for example,
            // the call to UseAuthentication must go after UseRouting, so that route information is available for
            // authentication decisions and before UseEndpoints so that users are authenticated before accessing the endpoints.

            // .USEROUTING() ===> Find Endpoint
            // Adds route matching to the middleware pipeline. This middleware looks at the set of endpoints
            // defined in the app, and selects the best match based on the request.

            // .USEENDPOINTS() ===> Execute Endpoint
            // Adds endpoint execution to the middleware pipeline.
            // It runs the delegate associated with the selected endpoint.

            // Adds EndpointRoutingMiddleware to the request handling pipeline.
            // The middleware looks for a corresponding Microsoft.AspNetCore.Http.Endpoint object.
            app.UseRouting();

            // Adds EndpointMiddleware to the request handling pipeline.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Person}/{action=GetAsync}");

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}