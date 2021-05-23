using Autenticacion.Dominio.Modelo.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Dominio.IServices.Command
{
    public interface ISesionesServiceCmd
    {
        LoginResponse IniciarSesion(CredencialesLogin request);

        TokenJWT validarTokenJWT(TokenJWT token);

        TokenJWT renovarToken(TokenJWT token);

        TokenJWT cambiarContrasena(TokenJWT token);

    }
}
