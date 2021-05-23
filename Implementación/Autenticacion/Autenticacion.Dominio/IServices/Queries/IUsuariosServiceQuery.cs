using Autenticacion.Dominio.Modelo.Command;
using Autenticacion.Dominio.Modelo.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Dominio.IServices.Queries
{
    public interface IUsuariosServiceQuery
    {
        UsuarioQuery consultarUsuario(string usuario);
        
        UsuarioQuery consultarUsuario(int id);

        IEnumerable<UsuarioQuery> getUsuarios(string usuario);


    }
}
