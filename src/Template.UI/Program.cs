using MudBlazor.Services;
using System.Globalization;
using Template.UI.Data;
using Template.UI.Configuration;
using Serilog.Enrichers.Standard.Http;
using Template.UI;

var builder = WebApplication.CreateBuilder(args);

RegisterServices(services: builder.Services, builder: builder);

var app = builder.Build();

Configure(app);

app.Run();

void RegisterServices(IServiceCollection services, WebApplicationBuilder builder)
{
    services.AddOptions();
    services.AddStandardHttp();
    services.Configure<AppSettings>(config: builder.Configuration);
    AppSettings appSettingsModel = builder.Configuration.Get<AppSettings>();
    services.AddScoped<IClipboardService, ClipboardService>();
    services.AddScoped<WeatherForecastService>();
    services.AddRazorPages();
    services.AddServerSideBlazor();
    services.AddMudServices();

    builder.Host.AddLoggingConfiguration(
    configurationModel: appSettingsModel);
}

void Configure(WebApplication app)
{
    UseDefaultCulture();
    app.UseStandardHttpMiddleware();
    app.UseStaticFiles();
    app.UseRouting();
    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");
}

void UseDefaultCulture()
{
    CultureInfo cultureInfo = new(name: "ru-RU");
    CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
    CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
}