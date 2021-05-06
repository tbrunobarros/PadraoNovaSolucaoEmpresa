using Empresa.NovaApi.API.Configuration;
using Empresa.NovaApi.API.Middleware;
using Empresa.NovaApi.Infra.CrossCutting.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Empresa.NovaApi.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IWebHostEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            Environment = environment;
            Configuration = builder.Build();
            Configuration.ConfigureSerilog();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddApiConfiguration(Configuration)
                //.AddJwtConfiguration(Configuration)
                //.AddAuthorizationConfiguration()
                .RegisterServices(Configuration)
                .AddSwaggerConfiguration();
            ;
        }

        public void Configure(IApplicationBuilder app)
        {
            app
                //.UseSerilog(loggerFactory)
                .UseGlobalExceptionHandler(Environment)
                .UseApiConfiguration(Environment)
               .UseSwaggerConfiguration();
        }
    }
}
