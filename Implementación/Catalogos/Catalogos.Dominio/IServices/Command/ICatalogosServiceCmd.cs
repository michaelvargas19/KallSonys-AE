using Catalogos.Dominio.Modelo.Command;
using Catalogos.Dominio.Modelo.Queries;

namespace Catalogos.Dominio.IServices.Command
{
    public interface ICatalogosServiceCmd
    {
        CatalogoQuery AgregarCatalogo(CatalogoCmd catalogoCmd);
    }
}
