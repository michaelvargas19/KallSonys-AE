using Autenticacion.Dominio.Modelo.Command;
using Autenticacion.Dominio.Modelo.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Dominio.IServices.Command
{
    public interface IUsuariosServiceCmd
    {
        UsuarioQuery registrarUsuario(UsuarioCmd usuario);

        UsuarioQuery actualizarUsuario(UsuarioCmd usuario);

        ResultadoCmd asignarRol(AsignarRolUserCmd rol);
        
        ResultadoCmd RemoverAsignarRol(AsignarRolUserCmd rol);

    }
}
