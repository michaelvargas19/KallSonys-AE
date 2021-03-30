using DominioAuth.Modelo;
using Microsoft.AspNetCore.Identity;
using PersistenciaAuth.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace PersistenciaAuth.Repositorios
{
    public class UsuariosRepository : RepositoryBase<Usuario>, IUsuariosRepository
    {
        private readonly UserManager<Usuario> userManager;
        private readonly ContextoAuthDB ContextDB;
        public UsuariosRepository(ContextoAuthDB ContextDB,
                                  UserManager<Usuario> userManager)
            : base(ContextDB)
        {
            this.ContextDB = ContextDB;
            this.userManager = userManager;
        }

        public IdentityResult Crear(Usuario usuario, Credenciales credenciales)
        {
            IdentityResult result = IdentityResult.Failed();

            try
            {
                if (credenciales.CredencialesValidas()) { 

                    result = userManager.CreateAsync(usuario, credenciales.Contrasena).Result;

                    if (result.Succeeded)
                    {
                        Usuario user = userManager.FindByNameAsync(usuario.UserName).Result;

                        TipoAutenticacion auth = ContextDB.TiposAutenticacion.Where(a => a.IdTipo == usuario.IdTipoAuth).Single();

                        //AccountManager
                        Claim claimAuth = new Claim(ClaimTypes.AuthenticationMethod, "Usuario y Contraseña");
                        Claim claimDomain = new Claim(ClaimTypes.StreetAddress, String.Empty);
                        Claim claimURL = new Claim(ClaimTypes.Uri, String.Empty);
                        Claim claimLocality = new Claim(ClaimTypes.Locality, String.Empty);

                        //Add Claims
                        result = userManager.AddClaimsAsync(user, new List<Claim> { claimAuth, claimDomain, claimURL, claimLocality }).Result;

                    }
                }

            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }
    }
}
