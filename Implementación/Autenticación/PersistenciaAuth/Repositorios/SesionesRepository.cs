using DominioAuth.Modelo;
using DominioAuth.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersistenciaAuth.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace PersistenciaAuth.Repositorios
{
    public class SesionesRepository : ISesionesRepository
    {
        private ContextoAuthDB DBContext;
        private readonly SignInManager<Usuario> signInManager;
        private readonly UserManager<Usuario> userManager;
       
        public SesionesRepository(ContextoAuthDB Context,
                                  SignInManager<Usuario> signInManager,
                                  UserManager<Usuario> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.DBContext = Context;
        }
        public SignInResult IniciarSesion(LoginRequest request)
        {
            SignInResult result = SignInResult.Failed;
            try
            {
                string user = request.Usuario;
                user = user.Contains("@") ? user.Split("@")[0] : user;
            
                Usuario usuario = userManager.FindByNameAsync(user).Result;

                if (usuario != null)
                {
                    result = signInManager.PasswordSignInAsync(user, request.Contrasena, false, false).Result;
                }

            }
            catch (Exception e) 
            {
                throw e;
            }

            return result;
            
        }


        public Usuario GetUsuario(string usuario)
        {
            Usuario user = null;
            try
            {
                
                user = userManager.FindByNameAsync(usuario).Result;

            }
            catch (Exception e)
            {
                throw e;
            }

            return user;

        }


        public IEnumerable<Claim> getClaims(string usuario, string idAplicacion)
        {
            IList<Claim> claims = new List<Claim>();
            bool hasRole = false;

            try
            {
                Usuario user = userManager.FindByNameAsync(usuario).Result;
                Aplicacion app = DBContext.Aplicaciones.Where(a => a.IdAplicacion == idAplicacion)
                                                       .Include(a=> a.Roles).FirstOrDefault();

                //User claims
                if ((user != null) && (app != null))
                {
                    claims = userManager.GetClaimsAsync(user).Result;

                    foreach (Rol rol in app.Roles)
                    {
                        if (signInManager.UserManager.IsInRoleAsync(user, rol.Name).Result)
                        {
                            hasRole = true;
                            claims.Add(new Claim(ClaimTypes.Role, rol.Display));
                            continue;
                        }
                    }

                    if (hasRole)
                    {
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserName));
                        claims.Add(new Claim(ClaimTypes.UserData, (user.Identificacion == null) ? "" : user.Identificacion.Trim()));
                        claims.Add(new Claim(ClaimTypes.Email, user.Email));
                        claims.Add(new Claim(ClaimTypes.GivenName, user.Nombres + " " + user.Apellidos));
                        claims.Add(new Claim(ClaimTypes.Surname, user.Cargo));
                    }
                    else
                    {
                        throw new Exception("El usuario no tiene permisos para iniciar sesión.");
                    }

                }
                else
                {
                    throw new Exception("La configuración no es correcta.");
                }

            }
            catch (Exception e)
            {
                throw e;
            }

            return claims;

        }

    }
}
