using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Empresa.NovaApi.Application.Services;
using Empresa.NovaApi.Infra.CrossCutting.IoC.Extensions;
using Empresa.NovaApi.Infra.CrossCutting.Options;
using Empresa.NovaApi.Application.Interfaces;

namespace Empresa.NovaApi.Infra.CrossCutting.IoC
{
    public static class Injector
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .RegisterMappings()
                .RegisterContexts()
                .RegisterSettings(configuration)
                .RegisterApplicationServices()
                .RegisterRepositories()
                .RegisterConfigServices()
                .RegisterHttpClients()
                ;

            return services;
        }
        private static IServiceCollection RegisterMappings(this IServiceCollection services)
        {

            return services;
        }
        private static IServiceCollection RegisterContexts(this IServiceCollection services)
        {

            return services;
        }
        private static IServiceCollection RegisterSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddConfiguration<AppSettings>(configuration);
            services.AddConfiguration<ConexaoBancoDados>(configuration, "ConnectionStrings");
            return services;
        }
        private static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IBancoDadosExemploService, BancoDadosExemploService>();


            return services;
        }
        private static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {

            return services;
        }
        private static IServiceCollection RegisterConfigServices(this IServiceCollection services)
        {
            //services.AddScoped<IUser, User>();

            return services;
        }

        private static IServiceCollection RegisterHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient<ISegurancaService, SegurancaService>();

            return services;
        }
    }
}
