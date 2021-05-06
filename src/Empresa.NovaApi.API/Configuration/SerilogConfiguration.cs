using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Empresa.NovaApi.API.Configuration
{
    public static class SerilogConfiguration
    {
        public static void ConfigureSerilog(this IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", "FaturamentoAPI")
                //.Enrich.WithMachineName()
                .WriteTo.Console()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 10)
                //.WriteTo.ApplicationInsights("48a70f78-c961-48b2-b036-99a058ed5fb1", TelemetryConverter.Traces, LogEventLevel.Debug)
                .CreateLogger();

        }

        public static IApplicationBuilder UseSerilog(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();

            app.UseSerilogRequestLogging();

            return app;
        }


    }
}
