using AutoMapper;
using cnet_paas;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using paas_inpo_calypsops_mocks.Dominio;
using cmes_webapi.Common.Configuration;
using cmes_webapi.Common.Services;
using cmes_webapi.Dominio;
using cmes_webapi.Filters;
using cmes_webapi.Logging;
using cmes_webapi.ServiceHelper;
using cmes_webapi.ServiceHelper.Contracts;
using cmes_webapi.ServiceRepository.OMS;
using cmes_webapi.Services;
using System;

namespace cmes_webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Service.SaveSettings();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Service.SaveSettings();

            services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
                options.Filters.Add<ValidatorFiltersTipoOperatoria>();

            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

            }).AddFluentValidation(fvc =>
            {
                fvc.DisableDataAnnotationsValidation = true;
                fvc.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMemoryCache();
            services.AddPaas(Configuration);

            //// Auto Mapper Configurations
            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile<DomainProfile>();
            //});

            //config.AssertConfigurationIsValid();
            services.ConfigureAutomapper();

            //Inyeccion de dependencia de servicios y serviceClient
            services.AddScoped<ITipoOperatoriaService, TipoOperatoriaService>();
            services.AddScoped<IOMSServicesTipoOperatoria, OMSServicesTipoOperatoria>();
        
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMapper, Mapper>();
            services.AddTransient<ILogger, Logger<LoggingBroker>>();
            services.AddTransient<ILoggingBroker, LoggingBroker>();
            //Para llamada a servicio REST 
            services.AddScoped<IRestServiceClient, RestServiceClient>();
         
            //Creo un health check
            services.AddHealthChecks()
                    .AddCheck<MepHealthCheck>("CalypsoPSACDIHealthCheck");

            services.Configure<HealthCheckPublisherOptions>(opt =>
            {
                opt.Delay = TimeSpan.FromMilliseconds(Configuration.GetValue<int>("DELAY"));
                opt.Timeout = TimeSpan.FromSeconds(Configuration.GetValue<int>("TIMEOUT"));
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1.0.0", new OpenApiInfo { Title = "paas-inpo-calypsops-acdi", Version = "v1.0.0" });

                //options.EnableAnnotations();
                //// use it if you want to hide Paths and Definitions from OpenApi documentation correctly
                //options.UseAllOfToExtendReferenceSchemas();

                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UsePaas();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
