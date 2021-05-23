using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Infraestructura.IRepositories.Command
{
    public interface IRepositorySessionesCmd
    {
        SignInResult IniciarSesion(string IdAplicacion, string Usuario, string Contrasena);
    }
}
