using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Globalization;
using System.IO.Compression;

namespace Empresa.NovaApi.API.Configuration
{
    public static class ApiConfiguration
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("Cors",
                    builder =>
                    {
                        builder.WithOrigins(configuration.GetSection("Client-angular:url").Value)
                                            .AllowAnyHeader()
                                            .AllowAnyMethod()
                                            .AllowCredentials();
                    });
            });

            services.AddMvc(
                option =>
                {
                    option.EnableEndpointRouting = true;

                    //option.Filters.Add(new AuthorizeFilter(policy));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                    options.SerializerSettings.NullValueHandling =
                        NullValueHandling.Ignore;
                }

                );

            return services
                .AddCompressaoDados()
                ;
        }

        


        public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();
            app.UseStaticFiles();

            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseCors("Cors");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }            

            app.UseRouting();
            app.UseEndpoints(x => x.MapControllers());

            return app
                .UseCompressao();
        }



        #region Configuração de Compressao de Dados
        
        //TODO: 

        //PRAAMANHA: Isso e importante
        public static IServiceCollection AddCompressaoDados(this IServiceCollection services)
        {
            // Configura o modo de compressão
            services.Configure<GzipCompressionProviderOptions>(
                options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
            });

            return services;
        }

        private static IApplicationBuilder UseCompressao(this IApplicationBuilder app) {

            // Ativa a compressão
            app.UseResponseCompression();

            return app;
        }


        #endregion

    }
}
