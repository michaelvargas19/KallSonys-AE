using Catalogos.Dominio.IServices.Command;
using Catalogos.Dominio.IServices.Queries;
using Catalogos.Dominio.IUnitOfWorks;
using Catalogos.Dominio.Services.Command;
using Catalogos.Dominio.Services.Queries;
using Catalogos.Dominio.Util;
using Catalogos.Infraestructura.IRepositories;
using Catalogos.Infraestructura.Repositories;
using Catalogos.Infraestructura.Repository;
using Catalogos.Infraestructura.SettinsDB;
using Catalogos.Infraestructura.UnitOfWorks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.IO;
using System.Reflection;

namespace Catalogos.API.Extensions
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



        public static void ConfigureMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
            
            services.AddSingleton<IMongoDbSettings>(serviceProvider =>
            serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

        }

        public static void Configureinterfaces(this IServiceCollection services)
        {
            //Services - Command
            services.AddScoped<ICatalogosServiceCmd, CatalogosServiceCmd>();
            services.AddScoped<IProductosServiceCmd, ProductosServiceCmd>();

            //Services - Command
            services.AddScoped<ICatalogosServiceQuery, CatalogosServiceQuery>();
            services.AddScoped<IProductosServiceQuery, ProductosServiceQuery>();

            //Init of work
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            //BD
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            services.AddScoped<IMongoContext, MongoContext>();

            //Utils
            services.AddScoped<IUtils, Dominio.Util.Utils>();
        }



        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Catálogos API",
                    Description = "API de Catálogos",
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
                    Description = "Inserte el JWT con Bearer en el campo de texto",
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
