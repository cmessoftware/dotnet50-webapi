using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using paas_inpo_calypsops_mocks.Dominio;

namespace cmes_webapi.Dominio
{
    public static class AutomapperExtensions
    {
        public static void ConfigureAutomapper(this IServiceCollection services)
        {
            var provider = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DomainProfile());
          
            });
            
            provider.CreateMapper();
            provider.AssertConfigurationIsValid();
            services.AddSingleton(provider);
        }
    }
}
