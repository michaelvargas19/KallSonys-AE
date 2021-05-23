using Autenticacion.Dominio.Modelo.Command;
using Autenticacion.Dominio.Modelo.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Dominio.IServices.Queries
{
    public interface IRolesServiceQuery
    {
        IEnumerable<RolQuery> rolesPorAplicacion(string idAplicacion);
        
        UsuarioQuery verRolesPorUsuario_Aplicacion(string usuario, string idAplicacion);

        UsuarioQuery verRolesPorUsuario(string usuario);

    }
}
