using Ordenes.Dominio.IRepositories;
using Ordenes.Dominio.IUnitOfWorks;
using Ordenes.Dominio.Modelo.Settings;
using Ordenes.Infraestructura.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using Ordenes.Infraestructura.UnitOfWork;
using Catalogos.Infraestructura.Repositories;
using Ordenes.Dominio.IServices;
using Ordenes.Infraestructura.Services;
using Confluent.Kafka;

namespace Ordenes.API.Extensions
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


            services.AddScoped(typeof(IService_Command<>), typeof(Service_Command<>));
            services.AddScoped(typeof(IService_Query<>), typeof(Service_Query<>));

            services.AddScoped(typeof(IMongoRepository_Command<>), typeof(MongoRepository_Command<>));
            services.AddScoped(typeof(IMongoRepository_Query<>), typeof(MongoRepository_Query<>));
            services.AddScoped<IMongoContext, MongoContext>();
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWorkI<>));
        }
        public static void ConfigureKafka(this IServiceCollection services, IConfiguration configuration)
        {
            var producerConfig = new ProducerConfig();
            configuration.Bind("KafkaSettings", producerConfig);
            producerConfig.SaslMechanism = SaslMechanism.Plain;
            producerConfig.SecurityProtocol = SecurityProtocol.SaslSsl;

            services.AddSingleton<ProducerConfig>(producerConfig);


            // ------ Consumer --------
            //services.AddHostedService<EventsKafka>();
            var consumerConfig = new ConsumerConfig();
            configuration.Bind("KafkaSettings", consumerConfig);
            consumerConfig.SaslMechanism = SaslMechanism.Plain;
            consumerConfig.SecurityProtocol = SecurityProtocol.SaslSsl;
            consumerConfig.GroupId = Guid.NewGuid().ToString();
            consumerConfig.AutoOffsetReset = AutoOffsetReset.Earliest;

            services.AddSingleton<ConsumerConfig>(consumerConfig);


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
