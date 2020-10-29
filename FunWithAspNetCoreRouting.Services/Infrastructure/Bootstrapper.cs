using FunWithAspNetCoreRouting.Services.Services;
using FunWithAspNetCoreRouting.Services.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FunWithAspNetCoreRouting.Services.Infrastructure
{
    public static class Bootstrapper
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
        }
    }
}