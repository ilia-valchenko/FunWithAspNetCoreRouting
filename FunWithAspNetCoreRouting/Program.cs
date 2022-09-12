using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FunWithAspNetCoreRouting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // The WebApplicationBuilder: IHostBuilder does the following things:
            // * configure application;
            // * add services;
            // * configure logging;
            // * configure environment;
            // * configure IHostBuilder and IWebHostBuilder.
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup_RouteConstraints>();
                });
    }
}