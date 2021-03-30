using DominioAuth.Modelo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersistenciaAuth.Interfaces;
using System;
using System.Linq;
using System.Security.Claims;

namespace PersistenciaAuth.Repositorios
{
    public class RolesRepository : RepositoryBase<Rol>, IRolesRepository
    {
        private readonly ContextoAuthDB ContextDB;
        private readonly RoleManager<Rol> managerRole;
        private readonly UserManager<Usuario> managerUser;

        public RolesRepository(ContextoAuthDB ContextDB, 
                               RoleManager<Rol> managerRole,
                               UserManager<Usuario> managerUser)
            : base(ContextDB)
    {
            this.ContextDB = ContextDB;
            this.managerRole = managerRole;
            this.managerUser = managerUser;

    }

        public Rol Crear(Rol rol)
        {
            rol.Name = rol.Display + "_" + rol.IdAplicacion;

            IdentityResult result = (this.managerRole.CreateAsync(rol)).Result;
            Aplicacion aplicacion = ContextDB.Aplicaciones.Where(a => a.IdAplicacion == rol.IdAplicacion && a.Estado == true && a.PermiteJWT == true)
                                                                         .Include(a => a.Roles).FirstOrDefault();

            if ((aplicacion != null) && !(aplicacion.Roles.Any(r=> r.Name == rol.Name)))
            {
                result = managerRole.CreateAsync(rol).Result
                    ;
                if (result.Succeeded)
                {
                    result = managerRole.AddClaimAsync(rol, new Claim(ClaimTypes.System, aplicacion.IdAplicacion)).Result;
                }
            }
            else
            {
                throw new Exception("Configuración inválida.");
            }

            return rol;
        }
}
}
