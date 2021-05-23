using Autenticacion.Dominio.IServices.Command;
using Autenticacion.Dominio.IServices.Queries;
using Autenticacion.Dominio.IUnitOfWorks;
using Autenticacion.Dominio.Services.Command;
using Autenticacion.Dominio.Services.Queries;
using Autenticacion.Dominio.UnitOfWorks;
using Autenticacion.Infraestructura;
using Autenticacion.Infraestructura.Entities;
using Autenticacion.Infraestructura.IRepositories.Command;
using Autenticacion.Infraestructura.IRepositories.Queries;
using Autenticacion.Infraestructura.Repositories.Command;
using Autenticacion.Infraestructura.Repositories.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Autenticacion.API.Extensions
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


        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Autenticación API",
                    Description = "API de Autenticación",
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
                    Description = "Inserte el JWT Con 'Bearer' en el campo de texto",
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


        public static void ConfigureInterfaces(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepositoryBaseQuery<>), typeof(RepositoryBaseQuery<>));
            services.AddScoped(typeof(IRepositorySessionesCmd), typeof(RepositorySessionesCmd));
            services.AddScoped(typeof(IRepositorySessionesQueries), typeof(RepositorySessionesQueries));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(typeof(IAplicacionesServiceCmd), typeof(AplicacionesServiceCmd));
            services.AddScoped(typeof(ILogServiceCmd), typeof(LogServiceCmd));
            //services.AddScoped(typeof(IRolesServiceCmd), typeof(RolesServiceCmd));
            services.AddScoped(typeof(IRoleIdentityRepository), typeof(RoleIdentityRepository));
            services.AddScoped(typeof(IUsuariosServiceCmd), typeof(UsuariosServiceCmd));
            services.AddScoped(typeof(IUserIdentityRepository), typeof(UserIdentityRepository));
            services.AddScoped(typeof(ISesionesServiceCmd), typeof(SesionesService));


            services.AddScoped(typeof(IAplicacionesServiceQuery), typeof(AplicacionesServiceQuery));
            services.AddScoped(typeof(IRolesServiceQuery), typeof(RolesServiceQuery));
            
        }


    }
}
