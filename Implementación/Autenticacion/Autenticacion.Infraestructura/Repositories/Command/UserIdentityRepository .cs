using Autenticacion.Infraestructura.Entities;
using Autenticacion.Infraestructura.IRepositories.Command;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion.Infraestructura.Repositories.Command
{
    public class UserIdentityRepository : IUserIdentityRepository
    {

        private readonly UserManager<Usuario> userManager;
        private readonly RoleIdentityRepository roleManager;
        private readonly ContextoAuthDB dbManager;
        private string IdApp;


        public UserIdentityRepository(IConfiguration configuration, 
                                      UserManager<Usuario> userManager,
                                      RoleManager<Rol> roleManager,
                                      ContextoAuthDB dbContext)
        {
            this.userManager = userManager;
            this.roleManager = new RoleIdentityRepository (configuration, roleManager, userManager,dbContext);
            this.dbManager = dbContext;
            this.IdApp = configuration["IdentifierAPP:Id"];
        }

        #region Asignar Rol

        public IdentityResult AsigRoleUser(string userName, int idRole)
        {
            try
            {
                IdentityResult result;

                Usuario user = userManager.FindByNameAsync(userName).Result;
                Rol rol = this.dbManager.Roles.Where(r => r.Id == idRole).FirstOrDefault();

                    if (user == null)
                    {
                        throw new Exception("Usuario inválido");
                    }
                
                    if (rol == null)
                    {
                        throw new Exception("Rol inválido");
                    }

                    if ( (roleManager.UserRoleExists(userName, idRole.ToString())) )
                    {
                        throw new Exception("El usuario ya cuenta con este rol");
                    }

                result = roleManager.CreateRelationUserRoleAsync(user.UserName, rol.Id).Result;

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public IdentityResult RemoveAsigRoleUser(string userName, int idRole)
        {
            try
            {
                IdentityResult result;

                Usuario user = userManager.FindByNameAsync(userName).Result;
                Rol rol = this.dbManager.Roles.Where(r => r.Id == idRole).FirstOrDefault();

                if (user == null)
                {
                    throw new Exception("Usuario inválido");
                }

                if (rol == null)
                {
                    throw new Exception("Rol inválido");
                }
                
                if( !(roleManager.UserRoleExists(userName, idRole.ToString())) )
                {
                    throw new Exception("El usuario no tiene asociado el rol especificado");
                }

                result = roleManager.UserRoleDelete(user.UserName, rol.Id).Result;

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion


        public IdentityResult CreateUser(Usuario usuario, string contrasena)
        {
            try
            {
                IdentityResult result = userManager.CreateAsync(usuario, contrasena).Result;

                if (result.Succeeded)
                {
                    Usuario appUser = userManager.FindByNameAsync(usuario.UserName).Result;

                    TipoAutenticacion auth = dbManager.TiposAutenticacion.Where(a => a.IdTipo == usuario.IdTipoAuth).Single();

                    //AccountManager
                    Claim claimAuth = new Claim(ClaimTypes.AuthenticationMethod, "Usuario y Contraseña");
                    Claim claimDomain = new Claim(ClaimTypes.StreetAddress, String.Empty);
                    Claim claimURL = new Claim(ClaimTypes.Uri, String.Empty);
                    Claim claimLocality = new Claim(ClaimTypes.Locality, String.Empty);

                    //if (auth.IdAD != null)
                    //{
                    //    Dire directory = dbManager.ActiveDirectory.Where(d => d.IdAD == auth.IdAD).Single();

                    //    //ActiveDirectory
                    //    if (!directory.IsCloud)
                    //    {
                    //        claimDomain = new Claim(ClaimTypes.StreetAddress, directory.Domain);
                    //        claimURL = new Claim(ClaimTypes.Uri, directory.Host);
                    //        claimAuth = new Claim(ClaimTypes.AuthenticationMethod, AutethienticationType.ActiveDirectory.ToString());
                    //    }
                    //    //AzureAD
                    //    else
                    //    {
                    //        claimDomain = new Claim(ClaimTypes.StreetAddress, directory.TenantId);
                    //        claimURL = new Claim(ClaimTypes.Uri, directory.ClientId);
                    //        claimAuth = new Claim(ClaimTypes.AuthenticationMethod, AutethienticationType.AzureAD.ToString());
                    //        claimLocality = new Claim(ClaimTypes.Locality, directory.Host);
                    //    }


                    //}

                    //Add Claims
                    result = userManager.AddClaimsAsync(appUser, new List<Claim> { claimAuth, claimDomain, claimURL, claimLocality }).Result;

                }

                return result;

            }
            catch (Exception e)
            {
                IdentityError error = new IdentityError();
                error.Code = e.Message;
                error.Description = e.StackTrace.ToString();

                IdentityResult result = IdentityResult.Failed(error);


                return result;
            }
        }

        public IEnumerable<Usuario> ReadAllUsers_WithOutManager()
        {
            try
            {
                //Identify Admin User
                IEnumerable<int> adminUsers = dbManager.Roles.Where(r => r.IdAplicacion == IdApp)
                                                        .Join(dbManager.UserRoles,
                                                            r => r.Id,
                                                            ur => ur.RoleId,
                                                            (r, ur) => ur.UserId
                                                        ).Distinct().ToList();
                //Find non-administrator Users
                IEnumerable<Usuario> users = userManager.Users.Where(u => !adminUsers.Any(i => i == u.Id));

                return users;
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        public IEnumerable<Usuario> ReadAllUsers_WithManager()
        {
            try
            {
                //Find All Users
                IEnumerable<Usuario> users = userManager.Users;
                return users;

            }
            catch (Exception e)
            {
                throw e;
            }

        }


        public Usuario ReadByNameUser(string userName)
        {
            try
            {
                Usuario user = userManager.FindByNameAsync(userName).Result;

                return user;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public Usuario ReadByIdUsers(string id)
        {
            try
            {
                Usuario user = userManager.FindByIdAsync(id).Result;

                return user;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        //public async Task<IdentityResult> UpdateUsersAsync(string userName, User userDTO)
        //{
        //    try
        //    {
        //        AppUser user = userManager.FindByNameAsync(userName).Result;
        //        IdentityResult result;

        //        if (user != null)
        //        {
        //            result = (userManager.UpdateAsync(Util.Util.castUserToIdentity(userDTO, user))).Result;

        //            if (result.Succeeded)
        //            {
        //                IList<Claim> claims = userManager.GetClaimsAsync(user).Result;
        //                claims = claims.Where(cl => (cl.Type == ClaimTypes.AuthenticationMethod ||
        //                                             cl.Type == ClaimTypes.StreetAddress ||
        //                                             cl.Type == ClaimTypes.Uri)).ToList();

        //                AppAuthType auth = dbManager.AuthType.Where(a => a.IdAuth == user.IdAuth).Single();

        //                user = userManager.FindByNameAsync(userName).Result;
        //                result = userManager.RemoveClaimsAsync(user, claims).Result;

        //                //AccountManager
        //                Claim claimAuth = new Claim(ClaimTypes.AuthenticationMethod, AutethienticationType.AccountManager.ToString());
        //                Claim claimDomain = new Claim(ClaimTypes.StreetAddress, String.Empty);
        //                Claim claimURL = new Claim(ClaimTypes.Uri, String.Empty);
        //                Claim claimLocality = new Claim(ClaimTypes.Locality, String.Empty);

        //                if (auth.IdAD != null)
        //                {
        //                    AppActiveDirectory directory = dbManager.ActiveDirectory.Where(d => d.IdAD == auth.IdAD).Single();

        //                    //ActiveDirectory
        //                    if (!directory.IsCloud)
        //                    {
        //                        claimDomain = new Claim(ClaimTypes.StreetAddress, directory.Domain);
        //                        claimURL = new Claim(ClaimTypes.Uri, directory.Host);
        //                        claimAuth = new Claim(ClaimTypes.AuthenticationMethod, AutethienticationType.ActiveDirectory.ToString());
        //                    }
        //                    //AzureAD
        //                    else
        //                    {
        //                        claimDomain = new Claim(ClaimTypes.StreetAddress, directory.TenantId);
        //                        claimURL = new Claim(ClaimTypes.Uri, directory.ClientId);
        //                        claimAuth = new Claim(ClaimTypes.AuthenticationMethod, AutethienticationType.AzureAD.ToString());
        //                        claimLocality = new Claim(ClaimTypes.Locality, directory.Host);
        //                    }
        //                }


        //                //Add Claims
        //                result = userManager.AddClaimsAsync(user, new List<Claim> { claimAuth, claimDomain, claimURL, claimLocality }).Result;

        //            }
        //        }
        //        else
        //        {
        //            IdentityError error = new IdentityError();
        //            error.Code = "UserNotFound";
        //            error.Description = "User don't exist";

        //            result = IdentityResult.Failed(error);
        //        }

        //        return result;

        //    }
        //    catch (Exception e)
        //    {
        //        return null;
        //    }

        //}


        //public IdentityResult UpdatePaths(AppActiveDirectory directory, AppUser user)
        //{
        //    IdentityResult result;
        //    IList<Claim> claims = userManager.GetClaimsAsync(user).Result;

        //    claims = claims.Where(cl => (cl.Type == ClaimTypes.StreetAddress ||
        //                                 cl.Type == ClaimTypes.Uri ||
        //                                 cl.Type == ClaimTypes.Locality ||
        //                                 cl.Type == ClaimTypes.AuthenticationMethod)).ToList();

        //    result = userManager.RemoveClaimsAsync(user, claims).Result;

        //    Claim claimDomain = new Claim(ClaimTypes.StreetAddress, String.Empty);
        //    Claim claimURL = new Claim(ClaimTypes.Uri, String.Empty);
        //    Claim claimLocality = new Claim(ClaimTypes.Locality, String.Empty);
        //    Claim claimAuth = new Claim(ClaimTypes.Locality, String.Empty);

        //    //ActiveDirectory
        //    if (!directory.IsCloud)
        //    {
        //        claimDomain = new Claim(ClaimTypes.StreetAddress, directory.Domain);
        //        claimURL = new Claim(ClaimTypes.Uri, directory.Host);
        //        claimAuth = new Claim(ClaimTypes.AuthenticationMethod, "ActiveDirectory");
        //    }
        //    //AzureAD
        //    else
        //    {
        //        claimDomain = new Claim(ClaimTypes.StreetAddress, directory.TenantId);
        //        claimURL = new Claim(ClaimTypes.Uri, directory.ClientId);
        //        claimLocality = new Claim(ClaimTypes.Locality, directory.Host);
        //        claimAuth = new Claim(ClaimTypes.AuthenticationMethod, "AzureAD");
        //    }

        //    //Add Claims
        //    result = userManager.AddClaimsAsync(user, new List<Claim> { claimDomain, claimURL, claimLocality, claimAuth }).Result;

        //    return result;

        //}



        public IdentityResult DeleteUser(string userName)
        {
            try
            {
                Usuario user = userManager.FindByNameAsync(userName).Result;
                IdentityResult result;

                if (user != null)
                {
                    result = (userManager.DeleteAsync(user)).Result;
                }
                else
                {
                    IdentityError error = new IdentityError();
                    error.Code = "UserNotFound";
                    error.Description = "El Usuario no existe";

                    result = IdentityResult.Failed(error);
                }
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }



        ////--------------------------------AD User
        //public Usuario FindUserAD(string user, string domain)
        //{
        //    using (var ADContext = new PrincipalContext(ContextType.Domain, domain))
        //    {

        //        UserPrincipal dto = UserPrincipal.FindByIdentity(ADContext, user);

        //        if (dto != null)
        //        {
        //            // Get the sid from the NT account name
        //            var sid = (SecurityIdentifier)new NTAccount(domain, user)
        //                          .Translate(typeof(SecurityIdentifier));

        //            // Get the directory entry for the LDAP service account
        //            var serviceEntry = new System.DirectoryServices.DirectoryEntry("LDAP://" + domain.Split('.')[0]);

        //            var mySearcher = new DirectorySearcher(serviceEntry)
        //            {
        //                Filter = string.Format("(&(ObjectSid={0}))", sid.Value)
        //            };

        //            var suserAD = mySearcher.FindOne().GetDirectoryEntry();

        //            if (dto != null)
        //            {
        //                User data = new User();
        //                data.Email = dto.EmailAddress;
        //                data.Description = dto.Description;
        //                data.FirstName = dto.GivenName;
        //                data.LastName = dto.Surname;
        //                data.PhoneNumber = dto.VoiceTelephoneNumber;
        //                data.NumIdentification = (suserAD.Properties["pager"] == null) ? "" : suserAD.Properties["pager"].Value.ToString();
        //                return data;
        //            }
        //            else
        //            {
        //                return null;
        //            }

        //        }
        //        else
        //        {
        //            return null;
        //        }

        //    }

        //    return null;
        //}



    }


}
