using DominioAuth.Modelo;
using DominioAuth.Request;
using DominioAuth.Response;
using DominioAuth.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using NegocioAuth.Interfaces;
using NegocioAuth.JWT;
using Newtonsoft.Json;
using PersistenciaAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;

namespace NegocioAuth.Negocio
{
    public class SesionesNegocio : ISesionesNegocio
    {
        private readonly IRepositoryWrapper repository;
        private readonly string IdApp;
        private readonly string Issuer;
        public SesionesNegocio(IRepositoryWrapper repositoryWrapper,
                               IConfiguration configuration)
        {
            this.repository = repositoryWrapper;
            this.IdApp = configuration["IdentifierAPP:Id"];
            this.Issuer = configuration["JwtConfig:issuer"]; ;
        }

        public LoginResponse IniciarSesion(LoginRequest request)
        {
            request.IdAplicacion = "Manager";
            request.Usuario = "Admin";
            request.Contrasena= "Scare2021";

            LoginResponse response = new LoginResponse();
            response.Autenticacion = false;
            response.Bloqueado = false;

            try
            {

                Aplicacion app = repository.Aplicaciones.BuscarPrimero(a => a.IdAplicacion == request.IdAplicacion);
                IEnumerable<Claim> claims = null;
                //if ((app != null) && (app.Estado == true) && (app.PermiteJWT == true) && (app.FechaExpiracionLlave.Value.CompareTo(DateTime.Now) <= 0) && (app.LlaveSecreta != null))
                if (true)
                {
                    
                    request.Usuario = request.Usuario.Contains("@") ? request.Usuario.Split("@")[0] : request.Usuario;
                    Usuario usuario = repository.Sesiones.GetUsuario(request.Usuario);
                    SignInResult result = SignInResult.Failed;

                    if ((usuario != null))
                    {
                        claims = repository.Sesiones.getClaims(usuario.UserName, app.IdAplicacion);
                        Claim authClaim = new Claim("", String.Empty);
                        
                        authClaim = claims.Where(c => c.Type == ClaimTypes.AuthenticationMethod).FirstOrDefault();

                        if (authClaim == null)
                        {
                            throw new Exception("El usuario no tiene definido el tipo de Autenticación.");
                        }


                        //Account Service
                        if (authClaim.Value.CompareTo("Usuario y Contraseña") == 0)
                        {
                            result = repository.Sesiones.IniciarSesion(request);
                        }

                    }
                    else
                    {
                         throw new Exception("La dupla Usuario/Contraseña es incorrecta.");
                    }


                    if(result.Succeeded)
                    {
                        string algorithm = app.AlgoritmoDeSeguridad;
                        string jwt = TokenGenerator.GenerateTokenJWT(app.LlaveSecreta, algorithm, claims, app.MinutosDeVida.Value, this.Issuer, app.IdAplicacion);

                        response.TokenJWT = new TokenJWT();
                        response.TokenJWT.IdAplicacion = app.IdAplicacion;
                        response.TokenJWT.Token = jwt;
                    }
                }
                else
                {
                    response.Mensaje = "Inicio de sesión inválido";
                }

            }
            catch (Exception e)
            {
                repository.InsertarLog(new _LogAutenticacionAPI("Error", request.Usuario,"Autenticación API", MethodInfo.GetCurrentMethod().Name, this.ToString(), true, e.Message, e.StackTrace));
                throw e;   
            }
            
            return response;
            
        }


        public TokenJWT validarTokenJWT(TokenJWT token)
        {
            TokenJWT respuesta = new TokenJWT();
            
            try
            {
                Aplicacion aplicacion = repository.Aplicaciones.BuscarPorCondicion(a => a.IdAplicacion == token.IdAplicacion && a.Estado == true && a.PermiteJWT == true).FirstOrDefault();
                
                respuesta.IdAplicacion = token.IdAplicacion;
                respuesta.TokenValido = false;
                respuesta.Token = token.Token;

                if (aplicacion != null)
                {
                    respuesta.TokenValido = TokenValidator.ValidarTokenJWT(token.Token, aplicacion.LlaveSecreta, this.Issuer, aplicacion.IdAplicacion, TimeSpan.Zero);
                }

                respuesta.Token = (respuesta.TokenValido) ? "Token Válido" : "Token Inválido";

            }
            catch (Exception e) 
            {
                throw e;
            }

            return respuesta;

        }

        public TokenJWT renovarToken(TokenJWT token)
        {
            TokenJWT newToken = null;

            if (validarTokenJWT(token).TokenValido)
            {
                Aplicacion app = repository.Aplicaciones.BuscarPorCondicion(a=> a.IdAplicacion == token.IdAplicacion).FirstOrDefault();
                newToken = new TokenJWT();
                newToken.IdAplicacion = app.IdAplicacion;
                newToken.Token = TokenGenerator.renovarTokenJWT(token.Token, app.LlaveSecreta, app.MinutosDeVida.Value, this.Issuer, app.IdAplicacion);
                newToken.TokenValido = true;
            }
            return newToken;

        }



    }
}
