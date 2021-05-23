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
    public interface IUserIdentityRepository
    {
        IdentityResult AsigRoleUser(string userName, int idRole);

        IdentityResult RemoveAsigRoleUser(string userName, int idRole);

        IdentityResult CreateUser(Usuario usuario, string contrasena);

        IEnumerable<Usuario> ReadAllUsers_WithOutManager();

        IEnumerable<Usuario> ReadAllUsers_WithManager();

        Usuario ReadByNameUser(string userName);

        Usuario ReadByIdUsers(string id);
        IdentityResult DeleteUser(string userName);


    }
}
