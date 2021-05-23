using Autenticacion.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Autenticacion.Infraestructura.IRepositories.Queries
{
    public interface IRepositorySessionesQueries
    {
        IEnumerable<Claim> getClaims(string usuario, string idAplicacion);

        Usuario GetUsuario(string usuario);
    }
}
