using Autenticacion.Infraestructura.Entities;
using Autenticacion.Infraestructura.ISpecification;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion.Infraestructura.IRepositories.Command
{
    public interface IRoleIdentityRepository
    {
        IEnumerable<Rol> GetAllRoles(Usuario usuario);

        Task<IdentityResult> CreateRoleAsync(string idAplicacion, string nombre, string display, string descripcion);

        Task<IdentityResult> CreateRelationUserRoleAsync(string userName, int idRole);

    }
}
