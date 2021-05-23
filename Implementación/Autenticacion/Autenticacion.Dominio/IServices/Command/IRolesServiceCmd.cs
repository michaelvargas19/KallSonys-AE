using Autenticacion.Dominio.Modelo.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Dominio.IServices.Command
{
    public interface IRolesServiceCmd
    {
        RolCmd registrarRol(RolCmd rol);

        RolCmd actualizarRol(RolCmd rol);


    }
}
