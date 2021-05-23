using Autenticacion.Dominio.Modelo.Command;
using Autenticacion.Dominio.Modelo.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Dominio.IServices.Command
{
    public interface IAplicacionesServiceCmd
    {
        AplicacionQuery actualizarAplicacion(AplicacionCmd aplicacion);

        ResultadoCmd asignarRol(AsignarRolAppCmd request);

        AplicacionQuery registrarAplicacion(AplicacionCmd aplicacion);

    }
}
