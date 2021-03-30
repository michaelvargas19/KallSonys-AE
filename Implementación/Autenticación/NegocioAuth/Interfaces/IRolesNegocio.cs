using DominioAuth.Modelo;
using System.Collections.Generic;

namespace NegocioAuth.Interfaces
{
    public interface IRolesNegocio : INegocioBase<Rol>
    {
        Aplicacion verRoles(string idAplicacion);
    }
}
