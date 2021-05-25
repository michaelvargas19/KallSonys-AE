using Autenticacion.Dominio.IServices.Command;
using Autenticacion.Dominio.IUnitOfWorks;
using Autenticacion.Dominio.Modelo.Command;
using Autenticacion.Dominio.Specification;
using Autenticacion.Infraestructura.Entities;
using Autenticacion.Infraestructura.IRepositories.Command;
using Autenticacion.Infraestructura.Specification;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace Autenticacion.Dominio.Services.Command
{
    public class SesionesService : ISesionesServiceCmd
    {
        private readonly string IdApp;
        private readonly string Issuer;
        private readonly IUnitOfWork _ufw;
        
        public SesionesService(IConfiguration configuration,
                               IUnitOfWork ufwAplicacion)
        {
            
            this.IdApp = configuration["IdentifierAPP:Id"];
            this.Issuer = configuration["JwtConfig:issuer"];
            this._ufw = ufwAplicacion;
            
        }

        public LoginResponse IniciarSesion(CredencialesLogin request)
        {
            LoginResponse response = new LoginResponse();
            response.Autenticacion = false;
            response.Bloqueado = false;

            
            try
            {
                                
                Aplicacion app = _ufw.RepositoryQueryAplicacion().Find(new AplicacionSpecification(request.IdAplicacion, true, true)).FirstOrDefault();

                IEnumerable<Claim> claims = null;
                if ((app != null) && (app.Estado == true) && (app.PermiteJWT == true) && (app.FechaExpiracionLlave.Value.CompareTo(DateTime.Now) >= 0) && (app.LlaveSecreta != null))
                {

                    request.Usuario = request.Usuario.Contains("@") ? request.Usuario.Split("@")[0] : request.Usuario;
                    Usuario usuario = _ufw.RepositoryQueryUsuario().Find(new UsuarioSpecification(request.Usuario)).FirstOrDefault();

                    SignInResult result = SignInResult.Failed;

                    if ((usuario != null))
                    {
                        claims = _ufw.RepositorySessionesQueries().getClaims(usuario.UserName, app.IdAplicacion);
                        Claim authClaim = new Claim("", String.Empty);

                        authClaim = claims.Where(c => c.Type == ClaimTypes.AuthenticationMethod).FirstOrDefault();

                        if (authClaim == null)
                        {
                            throw new Exception("El usuario no tiene definido el tipo de Autenticación.");
                        }


                        //Account Service
                        if (authClaim.Value.CompareTo("Usuario y Contraseña") == 0)
                        {
                            result = this._ufw.RepositorySessionesCmd().IniciarSesion(request.IdAplicacion, request.Usuario, request.Contrasena);
                        }

                    }
                    else
                    {
                        throw new Exception("La dupla Usuario/Contraseña es incorrecta.");
                    }


                    if (result.Succeeded)
                    {
                        string algorithm = _ufw.RepositoryQueryAlgoritmo().Find(new AlgoritmoSpecification(app.AlgoritmoDeSeguridad) ).FirstOrDefault().Valor;                        
                        string jwt = TokenGenerator.GenerateTokenJWT(app.LlaveSecreta, algorithm, claims, app.MinutosDeVida.Value, this.Issuer, app.IdAplicacion);

                        response.TokenJWT = new TokenJWT();
                        response.TokenJWT.IdAplicacion = app.IdAplicacion;
                        response.TokenJWT.Token = jwt;
                        
                        response.Mensaje = "Usuario Autenticado";
                    }
                }
                else
                {
                    response.Mensaje = "Inicio de sesión inválido";
                }
                
            }
            catch (Exception e)
            {
                _ufw.InsertarLog(new _LogAutenticacionAPI("Error", request.Usuario, "Autenticación API", MethodInfo.GetCurrentMethod().Name, this.ToString(), true, e.Message, e.StackTrace));
                throw e;
            }
            
            return response;

        }


        public TokenJWT validarTokenJWT(TokenJWT token)
        {
            TokenJWT respuesta = new TokenJWT();

            try
            {
                /*
                Aplicacion aplicacion = (Aplicacion)_ufw.RepositoryBaseCommand<Aplicacion>().Find(new AplicacionSpecification(token.IdAplicacion, true, true));

                respuesta.IdAplicacion = token.IdAplicacion;
                respuesta.TokenValido = false;
                respuesta.Token = token.Token;

                if (aplicacion != null)
                {
                    respuesta.TokenValido = TokenValidator.ValidarTokenJWT(token.Token, aplicacion.LlaveSecreta, this.Issuer, aplicacion.IdAplicacion, TimeSpan.Zero);
                }

                respuesta.Token = (respuesta.TokenValido) ? "Token Válido" : "Token Inválido";
                */
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
            /*
            if (validarTokenJWT(token).TokenValido)
            {
                Aplicacion app = (Aplicacion)_ufw.RepositoryBaseCommand<Aplicacion>().Find(new AplicacionSpecification(token.IdAplicacion));
                newToken = new TokenJWT();
                newToken.IdAplicacion = app.IdAplicacion;
                newToken.Token = TokenGenerator.renovarTokenJWT(token.Token, app.LlaveSecreta, app.MinutosDeVida.Value, this.Issuer, app.IdAplicacion);
                newToken.TokenValido = true;
            }
            */
            return newToken;

        }

        public TokenJWT cambiarContrasena(TokenJWT token)
        {
            throw new NotImplementedException();
        }
    }
}
