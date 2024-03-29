using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            // NOTE: I'm not sure I need to use AddMvc here. It's a REST API service. AddControllers should be enough.
            services.AddControllers();
            //services.AddMvc();

            // IMPORTANT!
            // When you call .AddMvc() you implicitly call (according to a source code) the following methods:
            // * .AddMvcCore() --> calls .AddRouting() and registers IControllerFactory, IActionInvokerFactory, IFilterProvider, IApplicationModelProvider, IActionDescriptorProvider, IActionSelector
            // * .AddApiExplorer() --> registers IApiDescriptionProvider, IApiDescriptionGroupCollectionProvider
            // * .AddAuthorization() --> calls .AddAuthorizationCore(configure) and .AddAuthorizationPolicyEvaluator()
            // * .AddFormatterMappings()
            // * .AddViews()
            // * .AddRazorViewEngine()
            // * .AddRazorPages()
            // * .AddCacheTagHelper()
            // * .AddDataAnnotations()
            // * .AddJsonFormatters()
            // * .AddCors()

            // IMPORTANT!
            // When you call .AddControllers() you implicitly call (according to a source code) the following methods:
            // * .AddMvcCore() --> calls .AddRouting()
            // * .AddApiExplorer()
            // * .AddAuthorization()
            // * .AddCors()
            // * .AddDataAnnotations()
            // * .AddFormatterMappings()

            // Add Swagger middleware.
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();

            // Adds routing middleware. It's a part of .AddMvc() in .NET Core 3.
            // So you don't need to add it manually.
            // What about .AddControllers()? Why we don't need to use .AddRouting()?
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

            // Swashbuckle relies on MVC's Microsoft.AspNetCore.Mvc.ApiExplorer to discover the routes and endpoints.
            // If the project calls AddMvc, routes and endpoints are discovered automatically.
            // When calling AddMvcCore, the AddApiExplorer method must be explicitly called.
            // For more information, see Swashbuckle, ApiExplorer, and Routing.

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

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
            // The information about a route is stored in an instance of the RouteData class.
            // You can find it in the RouteData property of the RouteContext class.
            app.UseRouting();

            // RouteData consist of:
            // * Values - dictionary of route segments
            // * DataTokens - additional route values
            // * Routers - collection of all routes which are used for matching. The last element in the collection is a route which is matched.

            // Actions are either conventionally-routed or attribute-routed.
            // * conventionally-routed: https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-5.0#cr
            // Conventional routing typically used with controllers and views.
            // * attribute-routed: https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-5.0#ar
            // Attribute routing used with REST APIs. If you're primarily interested in routing for REST APIs, jump to the Attribute routing for REST APIs section.

            // Adds EndpointMiddleware to the request handling pipeline.
            app.UseEndpoints(endpoints =>
            {
                // #1
                // .MapControllers() is enough for RESP API service.
                // It doesn't make any assumptions about routing. It will rely only on the user doing attribute routing.
                endpoints.MapControllers();

                // #2
                // Adds an endpoint to a specific action. Specifies a route with given [name], [pattern], [defaults], [constraints], [dataTokens].
                //endpoints.MapControllerRoute(string name, string pattern, [object defaults = null], [object dataTokens = null]);
                //endpoints.MapControllerRoute("default", "{controller=Person}/{action=GetAsync}");

                // #3
                //endpoints.MapDefaultControllerRoute();
                // It's basically the same as the line below. It's just a shorthands for the line below.
                // endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
            });
        }
    }
}