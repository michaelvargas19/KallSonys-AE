using Autenticacion.Dominio.Modelo.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Dominio.IServices.Command
{
    public interface ILogServiceCmd
    {
        void AgregarLog(LogCmd log);

        
    }
}
