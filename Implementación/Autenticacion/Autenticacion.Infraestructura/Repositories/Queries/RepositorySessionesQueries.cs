using Autenticacion.Infraestructura.Entities;
using Autenticacion.Infraestructura.IRepositories.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Autenticacion.Infraestructura.Repositories.Queries
{
    public class RepositorySessionesQueries : IRepositorySessionesQueries
    {
        private ContextoAuthDB DBContext;
        private readonly SignInManager<Usuario> signInManager;
        private readonly UserManager<Usuario> userManager;

        public RepositorySessionesQueries(ContextoAuthDB Context,
                                  SignInManager<Usuario> signInManager,
                                  UserManager<Usuario> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.DBContext = Context;
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
                                                       .Include(a => a.Roles).FirstOrDefault();

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
                        claims.Add(new Claim(ClaimTypes.Email, ((user.Email != null)? user.Email.Trim() : "" ) ));
                        claims.Add(new Claim(ClaimTypes.GivenName, user.Nombres + " " + user.Apellidos));
                        claims.Add(new Claim(ClaimTypes.Surname, ((user.Cargo != null)? user.Cargo.Trim() : "" ) ));
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
