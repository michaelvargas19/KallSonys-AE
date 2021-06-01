using Confluent.Kafka;
using Inventarios.API.Events;
using Inventarios.Dominio.IServices.Command;
using Inventarios.Dominio.IServices.Queries;
using Inventarios.Dominio.IUnitOfWorks;
using Inventarios.Dominio.Services.Command;
using Inventarios.Dominio.Services.Queries;
using Inventarios.Dominio.Util;
using Inventarios.Infraestructura.IRepositories;
using Inventarios.Infraestructura.Repositories;
using Inventarios.Infraestructura.Repository;
using Inventarios.Infraestructura.SettingsDB;
using Inventarios.Infraestructura.UnitOfWorks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Inventarios.API.Extensions
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
            //services.Configure<InventariosStorageDBSettings>(configuration.GetSection(nameof(InventariosStorageDBSettings)));
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
            services.AddSingleton<IMongoDbSettings>(sp =>
                    sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
        }



        public static void ConfigureKafka(this IServiceCollection services, IConfiguration configuration)
        {
            var producerConfig = new ProducerConfig();
            configuration.Bind("KafkaSettings", producerConfig);
            producerConfig.SaslMechanism = SaslMechanism.Plain;
            producerConfig.SecurityProtocol = SecurityProtocol.SaslSsl;

            services.AddSingleton<ProducerConfig>(producerConfig);


            // ------ Consumer --------
            services.AddHostedService<EventsKafka>();
            var consumerConfig = new ConsumerConfig();
            configuration.Bind("KafkaSettings", consumerConfig);
            consumerConfig.SaslMechanism = SaslMechanism.Plain;
            consumerConfig.SecurityProtocol = SecurityProtocol.SaslSsl;
            consumerConfig.GroupId = Guid.NewGuid().ToString();
            consumerConfig.AutoOffsetReset = AutoOffsetReset.Earliest; 
            
            services.AddSingleton<ConsumerConfig>(consumerConfig);


        }




        public static void ConfigureInterfaces(this IServiceCollection services)
        {
            //Services - Command
            services.AddScoped<ICatalogosServiceCmd, CatalogosServiceCmd>();
            services.AddScoped<IProductosServiceCmd, ProductosServiceCmd>();
            services.AddScoped<IInventariosServiceCmd, InventariosServiceCmd>();

            //Services - Query
            services.AddScoped<ICatalogosServiceQuery, CatalogosServiceQuery>();
            services.AddScoped<IProductosServiceQuery, ProductosServiceQuery>();
            services.AddScoped<IInventariosServiceQuery, InventariosServiceQuery>();

            //Init of work
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            //BD
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            services.AddScoped<IMongoContext, MongoContext>();

            //Utils
            services.AddScoped<IUtils, Utils>();
        }


        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Inventarios API",
                    Description = "API de Inventarios",
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
