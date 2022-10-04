using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using nombremicroservicio.Domain.Interfaces.Repository;
using nombremicroservicio.Domain.Interfaces.Services;
using nombremicroservicio.Entities.Entidades;
using nombremicroservicio.Infrastructure.Services;
using nombremicroservicio.Repository.DBContext;
using nombremicroservicio.Repository.Repositorios;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System;
using System.IO;
using System.Reflection;

namespace nombremicroservicio.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            #region Database
            services.AddDbContext<CreditoAutoDbContext>(options =>
              options.UseSqlServer(
                  Configuration.GetConnectionString("creditoAutos")));
            #endregion

            services.AddScoped<IBaseRepository<Marca>, BaseRepository<Marca>>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IVehiculoRepository, VehiculoRepository>();
            services.AddScoped<IPatioRepository, PatioRepository>();
            services.AddScoped<IClientePatioRepository, ClientePatioRepository>();
            services.AddScoped<ISolicitudCreditoRepository, SolicitudCreditoRepository>();
            services.AddScoped<IEjecutivoRepository, EjecutivoRepository>();
            services.AddScoped<IMarcaRepository, MarcaRepository>();
            services.AddScoped<IBaseRepository<Ejecutivo>, BaseRepository<Ejecutivo>>();

          


            #region OPENTELEMETRY
            string otelServer = Environment.GetEnvironmentVariable("OTEL_SERVER");
            var serviceName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;

            services.AddHttpContextAccessor();
            //if (!String.IsNullOrEmpty(otelServer))
            //{
            //    services.AddOpenTelemetryTracing(builder =>
            //       builder
            //      .AddAspNetCoreInstrumentation()
            //      .AddHttpClientInstrumentation()
            //      .AddConsoleExporter()
            //      .AddZipkinExporter(options =>
            //      {
            //          options.Endpoint = new Uri($"http://{otelServer}:9411/api/v2/spans");
            //      })
            //      .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
            //    );
            //}

            //services.AddLogging(logging =>
            //{
            //    logging.AddOpenTelemetry(options =>
            //    {
            //        options.AddConsoleExporter();
            //    });
            //});

            #endregion OPENTELEMETRY

            #region INFRASTRUCTURE
            services.AddTransient<ICargaInicial, CargaInicialServicio>();
            services.AddTransient<IEjecutivo, EjecutivoServicio>();
            services.AddTransient<IMarca, MarcaServicio>();
            services.AddTransient<ICliente, ClienteServicio>();
            services.AddTransient<IVehiculo, VehiculoServicio>();
            services.AddTransient<IPatio, PatioServicio>();
            services.AddTransient<IClientePatio, ClientePatioServicio>();
            services.AddTransient<ISolicitudCredito, SolicitudCreditoServicio>();


            #endregion INFRASTRUCTURE

            #region COMPATIBILITY
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            #endregion COMPATIBILITY

            #region HANDLING API VERSIONS
            services.AddApiVersioning(options => options.UseApiBehavior = true);
            services.AddApiVersioning(options => options.AssumeDefaultVersionWhenUnspecified = true);
            #endregion HANDLING API VERSIONS

            #region POLICY FOR CROSS DOMAIN
            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                                   .AllowAnyMethod()
                                                                   .AllowAnyHeader()));
            #endregion POLICY FOR CROSS DOMAIN

            services.AddControllers();
            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "nombremicroservicio",
                    Description = "Una descripcion del microservicio",
                    Contact = new OpenApiContact
                    {
                        Name = "Banco Pichincha",
                        Email = "devops@pichincha.com",
                        Url = new Uri("https://www.pichincha.com"),
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            #endregion Swagger
        }




        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();     
            }

            #region Inicializar Data
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;

                var initialiser = services.GetRequiredService<ICargaInicial>();

                initialiser.CargarArchivosIniciales();
            }
            #endregion
            #region SwaggerUI
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "nombremicroservicio API");
                c.RoutePrefix = "swagger";
                c.InjectStylesheet("/swagger/custom.css");
            });
            #endregion Swagger
            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("AllowAll");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
