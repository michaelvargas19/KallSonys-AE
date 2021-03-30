using DominioAuth.Modelo;
using Microsoft.AspNetCore.Identity;

namespace NegocioAuth.Interfaces
{
    public interface IUsuariosNegocio : INegocioBase<Usuario>
    {
        IdentityResult Crear(Usuario usuario, Credenciales credenciales);
    }
}
