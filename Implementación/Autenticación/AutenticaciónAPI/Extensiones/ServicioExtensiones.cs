using DominioAuth.Modelo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using NegocioAuth;
using NegocioAuth.Interfaces;
using NegocioAuth.Negocio;
using PersistenciaAuth;
using PersistenciaAuth.Interfaces;
using PersistenciaAuth.Repositorios;
using System;
using System.IO;
using System.Reflection;

namespace AutenticaciónAPI.Extensiones
{
    public static class ServicioExtensiones
    {

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connection = config["ConnectionStrings:DBAutenticacion"];
            services.AddDbContext<ContextoAuthDB>(options =>
                options.UseSqlServer(connection)
            );
        }


        public static void ConfigureIdentityEF_Core(this IServiceCollection services)
        {
            services.AddIdentity<Usuario, Rol>(options =>
            {
                //User options
                options.User.RequireUniqueEmail = false;

                //Login options
                options.SignIn.RequireConfirmedEmail = false;

                //Password options
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

            }).AddEntityFrameworkStores<ContextoAuthDB>().AddDefaultTokenProviders();
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<ISesionesRepository, SesionesRepository>();
            services.AddScoped<IAplicacionesRepository, AplicacionesRepository>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigureNegocioWrapper(this IServiceCollection services)
        {
            services.AddScoped<ILogNegocio, LogNegocio>();
            services.AddScoped<ISesionesNegocio, SesionesNegocio>();
            services.AddScoped<INegocioWrapper, NegocioWrapper>();
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Autenticación API",
                    Description = "API de autenticación",
                    //TermsOfService = new Uri(""),
                    Contact = new OpenApiContact
                    {
                        Name = "PUJ",
                        Email = "email@puj.org.co",
                        Url = new Uri("https://puj.org.co/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "License",
                        Url = new Uri("https://puj.org.co/politica-privacidad/"),
                    }

                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Inserte el JWT bon Bearer en el campo de texto",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                    },
                    new string[] { }
                }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });
        }


    }
}
