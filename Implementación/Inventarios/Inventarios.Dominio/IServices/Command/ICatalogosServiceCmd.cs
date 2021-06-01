using Inventarios.Dominio.Modelo.Command;
using Inventarios.Dominio.Modelo.Queries;

namespace Inventarios.Dominio.IServices.Command
{
    public interface ICatalogosServiceCmd
    {
        CatalogoQuery AgregarCatalogo(CatalogoCmd catalogoCmd);

        CatalogoQuery ActualizarCatalogo(CatalogoCmd catalogoCmd);


    }
}
