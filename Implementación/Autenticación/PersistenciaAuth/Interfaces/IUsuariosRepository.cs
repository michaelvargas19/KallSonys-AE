using DominioAuth.Modelo;
using Microsoft.AspNetCore.Identity;


namespace PersistenciaAuth.Interfaces
{
    public interface IUsuariosRepository : IRepositoryBase<Usuario>
    {
        IdentityResult Crear(Usuario usuario, Credenciales credenciales);
    }
}
