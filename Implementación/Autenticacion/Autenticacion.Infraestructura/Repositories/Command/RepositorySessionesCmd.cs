using Autenticacion.Infraestructura.Entities;
using Autenticacion.Infraestructura.IRepositories.Command;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Infraestructura.Repositories.Command
{
    public class RepositorySessionesCmd : IRepositorySessionesCmd
    {

        private readonly SignInManager<Usuario> signInManager;
        private readonly UserManager<Usuario> userManager;

        public RepositorySessionesCmd(
                                  SignInManager<Usuario> signInManager,
                                  UserManager<Usuario> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;

        }
        public SignInResult IniciarSesion(string IdAplicacion, string userName, string contrasena)
        {
            SignInResult result = SignInResult.Failed;
            try
            {
                string user = userName;
                user = user.Contains("@") ? user.Split("@")[0] : user;

                Usuario usuario = userManager.FindByNameAsync(user).Result;

                if (usuario != null)
                {
                    result = signInManager.PasswordSignInAsync(user, contrasena, false, false).Result;
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
