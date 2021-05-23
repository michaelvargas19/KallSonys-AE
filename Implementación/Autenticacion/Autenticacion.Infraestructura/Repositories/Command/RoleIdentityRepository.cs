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
    public class RoleIdentityRepository : IRoleIdentityRepository
    {
        private readonly RoleManager<Rol> roleManager;
        private readonly UserManager<Usuario> userManager;
        private readonly ContextoAuthDB dbManager;
        private string IdApp;



        public RoleIdentityRepository(IConfiguration configuration, RoleManager<Rol> managerR,
                                      UserManager<Usuario> managerU, ContextoAuthDB dbContext)
        {
            this.userManager = managerU;
            this.roleManager = managerR;
            this.dbManager = dbContext;
            this.IdApp = configuration["IdentifierAPP:Id"];
        }

        #region Manager and PowerUser

        public async Task<IdentityResult> CreateRoleAsync(string idAplicacion, string nombre, string display, string descripcion)
        {
            try
            { 
                Rol rol = new Rol();
                rol.Name = nombre + idAplicacion;
                rol.IdAplicacion = idAplicacion;
                rol.Display = display;
                rol.Descripcion = descripcion;

                IdentityResult result = (roleManager.CreateAsync(rol)).Result;

                if (result.Succeeded)
                {
                    result = roleManager.AddClaimAsync(rol, new Claim(ClaimTypes.System, idAplicacion)).Result;
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

        public async Task<IdentityResult> CreateRelationUserRoleAsync(string userName, int idRole)
        {
            try
            {
                Usuario usuario = dbManager.Users.Where(u => u.UserName == userName).Single();
                Rol roleApp = dbManager.Roles.Where(r => r.Id == idRole).Single();

                if ((usuario != null) && (roleApp != null))
                {

                    IdentityUserRole<int> role = new IdentityUserRole<int>();
                    role.RoleId = roleApp.Id;
                    role.UserId = usuario.Id;

                    dbManager.UserRoles.Add(role);
                    await dbManager.SaveChangesAsync();
                    return IdentityResult.Success;
                }
                else
                {
                    return IdentityResult.Failed(null);
                }

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




        public IEnumerable<Rol> GetAllRoles(Usuario usuario)
        {
            try
            {
                List<Rol> roles = dbManager.UserRoles.Where(ur => ur.UserId == usuario.Id)
                                   .Join(dbManager.Roles, u => u.RoleId, r => r.Id, (u, r) => r).ToList();

                return roles;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<Rol>> ReadAllRolesAsync()
        {
            try
            {
                IEnumerable<Rol> roles = roleManager.Roles.Where(r => r.IdAplicacion != IdApp).AsEnumerable();
                
                return roles;
            }
            catch (Exception e)
            {
                throw e;    
            }

        }


        public async Task<Rol> ReadByIdRoleAsync(string idRole)
        {
            try
            {
                Rol role = roleManager.FindByIdAsync(idRole).Result;

                return role;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<IEnumerable<Usuario>> ReadAllUsersRolesAsync()
        {
            try
            {
                ////Identify Admin User
                //IEnumerable<int> adminUsers = dbManager.Roles.Where(r => r.IdAplicacion == IdApp)
                //                                              .Join(dbManager.UserRoles,
                //                                                    r => r.Id,
                //                                                    ur => ur.RoleId,
                //                                                    (r, ur) => ur.UserId
                //                                              ).Distinct().ToList();

                ////Find non-administrator Users
                //IEnumerable<Usuario> users = dbManager.Users.Where(u => !adminUsers.Any(i => i == u.Id))
                //                             .Select(u => new Usuario()
                //                             {
                //                                 Id = u.Id,
                //                                 UserName = u.UserName,
                //                                 Email = u.Email,
                //                                 Roles = (dbManager.UserRoles.Where(x => x.UserId == u.Id)
                //                                        .Join(dbManager.Roles.Where(r => r.IdAplicacion != IdApp),
                //                                            ur => ur.RoleId,
                //                                            r => r.Id,
                //                                            (ur, r) => new Rol
                //                                            {
                //                                                Id = r.Id,
                //                                                IdAplicacion =
                //                                                    (dbManager.Aplicaciones.Where(a => a.IdAplicacion == r.IdAplicacion)
                //                                                                            .Select(a => a.Nombre).Single()),
                //                                                Name = r.Name,
                //                                                Display = r.Display
                //                                            }
                //                                        ).OrderBy(x => x.Name).OrderBy(x => x.IdAplicacion).ToList()
                //                                )
                //                             }).ToList();

                ////return users;
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        //value1 -> UserName
        //value2 -> idRole
        public bool UserRoleExists(string value1, string value2)
        {
            try
            {
                if ((value1.Length > 0) && (value2.Length > 0))
                {
                    int roleId = Int32.Parse(value2);

                    Usuario user = dbManager.Users.Where(u => u.UserName == value1).Single();

                    return dbManager.UserRoles.Any(u => u.UserId == user.Id && u.RoleId == roleId);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IdentityResult> UpdateRolesAsync(string idRole, Rol rol)
        {
            try
            {
                Rol role = roleManager.FindByIdAsync(idRole).Result;
                IdentityResult result;

                if (role != null)
                {
                    result = roleManager.UpdateAsync(rol).Result;
                }
                else
                {

                    IdentityError error = new IdentityError();
                    error.Code = "RoleNotFound";
                    error.Description = "El rol no existe";

                    result = IdentityResult.Failed(error);
                }

                //return result;
                return null;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<IdentityResult> DeleteRolesAsync(string idRole)
        {
            try
            {
                Rol role = roleManager.FindByIdAsync(idRole).Result;
                IdentityResult result;

                if (role != null)
                {
                    result = (roleManager.DeleteAsync(role)).Result;
                }
                else
                {
                    IdentityError error = new IdentityError();
                    error.Code = "RoleNotFound";
                    error.Description = "El rol no existe";

                    result = IdentityResult.Failed(error);
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        //value1 -> UserName
        //value2 -> idRole
        public async Task<IdentityResult> UserRoleDelete(string value1, int value2)
        {
            try
            {
                if ((value1.Length > 0) && (value2 > 0))
                {
                    int roleId = value2;
                    Usuario user = dbManager.Users.Where(u => u.UserName == value1).Single();
                    IdentityUserRole<int> role = new IdentityUserRole<int>();

                    role.RoleId = roleId;
                    role.UserId = user.Id;

                    dbManager.UserRoles.Remove(role);
                    await dbManager.SaveChangesAsync();

                    return IdentityResult.Success;

                }
                else
                {
                    IdentityError error = new IdentityError();
                    error.Code = "Error";
                    error.Description = "Error";

                    return IdentityResult.Failed(error);
                }
            }
            catch (Exception e)
            {
                IdentityError error = new IdentityError();
                error.Code = e.Message;

                return IdentityResult.Failed(error);
            }
        }

        #endregion

        #region PowerUser
        public async Task<IEnumerable<Rol>> ReadAllRolesManagerAsync()
        {
            try
            {
                IEnumerable<Rol> roles = roleManager.Roles.Where(r => r.IdAplicacion == IdApp).AsEnumerable();
                List<Rol> list = new List<Rol>();

                foreach (Rol r in roles)
                {
                    //list.Add(Util.Util.convertRoleToDTO(r));
                }

                return list;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<IEnumerable<Usuario>> ReadAllUsersManageAsync()
        {
            try
            {
                //Find all Users
                //IEnumerable<Usuario> users = dbManager.Users
                //                            .Select(u => new Usuario()
                //                            {
                //                                Id = u.Id,
                //                                UserName = u.UserName,
                //                                Email = u.Email,
                //                                Roles = (dbManager.UserRoles.Where(x => x.UserId == u.Id)
                //                                    .Join(dbManager.Roles,
                //                                        ur => ur.RoleId,
                //                                        r => r.Id,
                //                                        (ur, r) => new Rol
                //                                        {
                //                                            Id = r.Id,
                //                                            IdAplicacion =
                //                                                (dbManager.Aplicaciones.Where(a => a.IdAplicacion == r.IdAplicacion)
                //                                                                        .Select(a => a.Nombre).Single()),
                //                                            Name = r.Name,
                //                                            Display = r.Display
                //                                        }
                //                                    ).OrderBy(x => x.Name).OrderBy(x => x.IdAplicacion).ToList()
                //                            )
                //                            }).ToList();
                //return users;
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

    }

}
