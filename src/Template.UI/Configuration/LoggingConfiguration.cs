using Serilog;

namespace Template.UI.Configuration
{
    internal static class LoggingConfiguration
    {
        public static void AddLoggingConfiguration(
            this ConfigureHostBuilder hostBuilder,
            AppSettings configurationModel)
        {
            hostBuilder.UseSerilog(
                configureLogger: (context, services, configuration) =>
                    configuration.ReadFrom.Configuration(configuration: context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.WithCorrelationId()
                    .Enrich.WithStandardMq()
                    .Enrich.WithStandardHttp(serviceType: configurationModel.ServiceType.ToString()));
        }
    }
}