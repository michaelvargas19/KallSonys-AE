using Autenticacion.Infraestructura.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Autenticacion.Infraestructura
{
    public class ContextoAuthDB : IdentityDbContext<Usuario, Rol, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, Token>
    {
        public ContextoAuthDB(DbContextOptions<ContextoAuthDB> options)
            : base(options)
        {
        }

        public DbSet<AlgoritmoDeSeguridad> AlgoritmosDeSeguridad { get; set; }

        public DbSet<Aplicacion> Aplicaciones { get; set; }

        public DbSet<TipoAutenticacion> TiposAutenticacion { get; set; }

        public DbSet<Token> Tokens { get; set; }

        public DbSet<_LogAutenticacionAPI> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TipoAutenticacion>().HasData(
                            new TipoAutenticacion() { IdTipo = 1, Autenticacion = "Usuario y Contraseña", EsDirectorioActivo = false, IdAD = null }
                         );

            //Insert App
            builder.Entity<Aplicacion>().HasData(
                            new Aplicacion() { IdAplicacion = "Manager", Nombre = "Account Service", EmailContacto = "michavarg9@gmail.com", Estado = true, PermiteJWT = true, EstadoLlave = true, AlgoritmoDeSeguridad = "HmacSha512", LlaveSecreta = "Secret_Key", MinutosDeVida = 60, FechaExpiracionLlave = DateTime.Now.AddYears(2) }
                         ); ;

            //Insert Role
            builder.Entity<Rol>().HasData(
                            new Rol() { Id = 1, Name = "PowerUser", NormalizedName = "POWERUSER", IdAplicacion = "Manager", Display = "PowerUser", Descripcion = "Usuario con permisos Full" }
                         );

            builder.Entity<Rol>().HasData(
                            new Rol() { Id = 2, Name = "Administrador", NormalizedName = "ADMINISTRADOR", IdAplicacion = "Manager", Display = "Administrador", Descripcion = "Usuario con permisos de Admin" }
                         );

            builder.Entity<Rol>().HasData(
                            new Rol() { Id = 3, Name = "Cliente", NormalizedName = "CLIENTE", IdAplicacion = "Manager", Display = "Cliente", Descripcion = "Cliente del sistema" }
                         );

            builder.Entity<Rol>().HasData(
                            new Rol() { Id = 4, Name = "Proveedor", NormalizedName = "PROVEEDOR", IdAplicacion = "Manager", Display = "Proveedor", Descripcion = "Proveedor del sistema" }
                         ); 

            //Insert Role Claims--> Identificador de la APP
            builder.Entity<IdentityRoleClaim<int>>().HasData(
                            new
                            { Id = 1, RoleId = 1, ClaimType = ClaimTypes.System, ClaimValue = "Manager" }
                         );

            builder.Entity<IdentityRoleClaim<int>>().HasData(
                            new
                            { Id = 2, RoleId = 2, ClaimType = ClaimTypes.System, ClaimValue = "Manager" }
                         );

            builder.Entity<IdentityRoleClaim<int>>().HasData(
                            new
                            { Id = 3, RoleId = 3, ClaimType = ClaimTypes.System, ClaimValue = "Manager" }
                         );

            builder.Entity<IdentityRoleClaim<int>>().HasData(
                            new
                            { Id = 4, RoleId = 4, ClaimType = ClaimTypes.System, ClaimValue = "Manager" }
                         );


            //Insert User
            builder.Entity<Usuario>().HasData(
                            new Usuario()
                            {
                                Id = 1,
                                UserName = "Admin",
                                NormalizedUserName = "ADMIN",
                                Email = "admin@admin.org.co",
                                NormalizedEmail = "ADMIN@ADMIN.ORG.CO",
                                EmailConfirmed = true,
                                PhoneNumberConfirmed = false,
                                TwoFactorEnabled = false,
                                LockoutEnabled = false,
                                AccessFailedCount = 0,
                                Nombres = "Admin",
                                Apellidos = "",
                                IdTipoAuth = 1,
                                PasswordHash = "AQAAAAEAACcQAAAAEDvsJrU5P2uO7jfhKVQTK2rMCwYlOAoWC3AzIGB+iktmo8A2515Utzul5+KXfWEjqQ==",
                                SecurityStamp = "XVMFBE37LCN4TNGMZSHLPHBV7FIVHBQG",
                                Cargo = "Administrador del sistema",
                                Description = "Administrador del sistema de autenticación",
                                Organizacion = "PUJ",
                                EsExterno = false,
                            });



            //Insert User Claims
            builder.Entity<IdentityUserClaim<int>>().HasData(
                            new
                            {
                                Id = 1,
                                UserId = 1,
                                ClaimType = ClaimTypes.AuthenticationMethod,
                                ClaimValue = "Usuario y Contraseña"
                            }
                         );

            builder.Entity<IdentityUserClaim<int>>().HasData(
                            new
                            {
                                Id = 2,
                                UserId = 1,
                                ClaimType = ClaimTypes.StreetAddress,
                                ClaimValue = String.Empty
                            });

            builder.Entity<IdentityUserClaim<int>>().HasData(
                            new
                            {
                                Id = 3,
                                UserId = 1,
                                ClaimType = ClaimTypes.Uri,
                                ClaimValue = String.Empty
                            });

            builder.Entity<IdentityUserClaim<int>>().HasData(
                            new
                            {
                                Id = 4,
                                UserId = 1,
                                ClaimType = ClaimTypes.Locality,
                                ClaimValue = String.Empty
                            });


            //Insert User-Role Relation
            builder.Entity<IdentityUserRole<int>>().HasData(
                            new { UserId = 1, RoleId = 1 }
                         );


            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "EcdsaSha256", Valor = "ES256" });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "EcdsaSha384", Valor = "ES384" });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "EcdsaSha512", Valor = "ES512" });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "HmacSha384", Valor = "HS384" });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "HmacSha512", Valor = "HS512" });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "None", Valor = "none" });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "RsaSha256", Valor = "RS256" });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "RsaSha384", Valor = "RS384" });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "RsaSha512", Valor = "RS512" });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "RsaSsaPssSha256", Valor = "PS256" });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "RsaSsaPssSha384", Valor = "PS384" });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "RsaSsaPssSha512", Valor = "PS512" });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "Aes128CbcHmacSha256", Valor = "A128CBC-HS256" });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "HmacSha256", Valor = "HS256" });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "Aes192CbcHmacSha384", Valor = "A192CBC-HS384" });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "Aes128KW", Valor = "A128KW" });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "Aes256KW", Valor = "A256KW" });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "RsaOAEP", Valor = "RSA-OAEP" });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "Sha256", Valor = "SHA256" });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "Sha384", Valor = "SHA384" });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "Sha512", Valor = "SHA512" });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "RsaPKCS1", Valor = "RSA1_5" });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "Aes256CbcHmacSha512", Valor = "A256CBC-HS512" });

        }

        public void Log(_LogAutenticacionAPI log)
        {
            if (!string.IsNullOrEmpty(log.Metodo))
            {
                this.Logs.Add(log);
                this.SaveChanges();
            }
        }
    }
}
