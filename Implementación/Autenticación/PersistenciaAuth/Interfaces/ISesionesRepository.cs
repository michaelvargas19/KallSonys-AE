using DominioAuth.Modelo;
using DominioAuth.Request;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace PersistenciaAuth.Interfaces
{
    public interface ISesionesRepository
    {
        SignInResult IniciarSesion(LoginRequest request);
        
        IEnumerable<Claim> getClaims(string usuario, string idAplicacion);

        Usuario GetUsuario(string usuario);


    }
}
