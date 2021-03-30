using DominioAuth.Modelo;
using PersistenciaAuth.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersistenciaAuth
{
    public interface IRepositoryWrapper
    {
        void InsertarLog(_LogAutenticacionAPI log);
        
        ContextoAuthDB ContextDB { get; }
        ILogRepository Log { get; }
        ISesionesRepository Sesiones { get; }
        IAplicacionesRepository Aplicaciones { get; }
        IRolesRepository Roles { get; }
        IUsuariosRepository Usuarios { get; }
    }
}
