using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

namespace Empresa.NovaApi.API.Middleware
{
    public static class ExceptionMiddleware
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (exceptionHandlerFeature != null)
                    {
                        var problemDetails = new ProblemDetails
                        {
                            Instance = context.Request.HttpContext.Request.Path
                        };

                        context.Response.StatusCode = context.Response.StatusCode;
                        context.Response.ContentType = "application/problem+json";

                        problemDetails.Title = exceptionHandlerFeature.Error.Message;
                        problemDetails.Status = StatusCodes.Status500InternalServerError;
                        //problemDetails.Detail = exceptionHandlerFeature.Error.Demystify().ToString();
                        problemDetails.Detail = exceptionHandlerFeature.Error.ToString();

                        Serilog.Log.ForContext("Operacao", "Processamento", destructureObjects: true)

                                //.ForContext("CorrelationID", correlationContextAccessor.CorrelationContext.CorrelationId, destructureObjects: true)                                
                                .ForContext("teste", $"thiago souza bruno", destructureObjects: true)
                                .Error("Erro: {Erro}", JsonConvert.SerializeObject(problemDetails));

                        if (env.IsDevelopment())
                        {
                            problemDetails.Detail = "Problema no processamento.";
                        }

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails));
                    }
                });

            });

            return app;
        }
    }
}
