using DominioAuth.Request;
using DominioAuth.Response;
using DominioAuth.Tokens;

namespace NegocioAuth.Interfaces
{
    public interface ISesionesNegocio
    {
        LoginResponse IniciarSesion(LoginRequest request);

        TokenJWT validarTokenJWT(TokenJWT token);

        TokenJWT renovarToken(TokenJWT token);

    }
}
