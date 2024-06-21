using Core.AspNetCore.Builder;
using MassTransit;
using MassTransit.Definition;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Polly;
using Serilog.Enrichers.Standard.Http;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Template.App.Consumers;
using Template.App.Example;
using Template.App.Repositories;
using Template.Domain.Interfaces;

namespace Template.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public const string ServiceType = "2000";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<AppSettings>(Configuration);
            var configuration = Configuration.Get<AppSettings>();

            services.AddDbContext< IRepository < Domain.Entities.TemplateEntity > ,DbRepository >(x =>
                x.UseNpgsql(configuration.ConnectionStrings.PgSqlConnection));

            services.AddHttpClient<IService, Repository>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration.ApiUrl))
                .AddTransientHttpErrorPolicy(policyBuilder =>
                    policyBuilder.WaitAndRetryAsync(2, _ => TimeSpan.FromSeconds(1)))
                .AddTransientHttpErrorPolicy(builder => builder.CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 3,
                    durationOfBreak: TimeSpan.FromMinutes(10)
                ));

            services.AddMassTransit(x =>
            {
                x.AddConsumer<TemplateConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration.ConnectionStrings.RabbitConnection);
                    cfg.ConfigureEndpoints(context, SnakeCaseEndpointNameFormatter.Instance);

                });
            });
            services.AddMassTransitHostedService();

            services.AddHealthChecks();

            services.AddStandardHttp();

            services.AddControllers().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            });
            services.AddSwaggerGen(options =>
            {
                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                options.CustomOperationIds(e => $"{Regex.Replace(e.RelativePath, "{|}", "")}{e.HttpMethod}");
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = GetType().Namespace,
                    Version = "v1",
                    Contact = new OpenApiContact()
                    {
                        Url = new Uri("http://docpo2.ru/doku.php?id=отделы:сервисы:standart:стандарт_по_написанию_инструкций"),
                    },
                    Description = $"Service {ServiceType}"
                });
                var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));

                options.SchemaFilter<ExampleSchemaFilter>();

            });
            services.AddSwaggerGenNewtonsoftSupport();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var cultureInfo = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            if (!env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger(o => o.SerializeAsV2 = true);
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{GetType().Namespace} v1");
                });
            }

            app.UseHttpExceptionHandlerMiddleware();

            app.UseStandardHttpMiddleware();

            app.UseRouting();

            app.UseHealthChecks("/api/template/checkstate");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
