using Ordenes.Dominio.IServices;
using Ordenes.Dominio.IUnitOfWorks;
using Ordenes.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordenes.Infraestructura.Services
{
    public class Services_EstadosItem_command : IService_Estados_command<Orden_Item_Ref_Estados>
    {
        public async Task<Orden_Item_Ref_Estados> AgregarEstadoOrden(Orden_Item_Ref_Estados estado, IUnitOfWork<Orden_Item_Ref_Estados> _unitOfWork)
        {

            var existeEstado = _unitOfWork.Service_Queries<Orden_Item_Ref_Estados>().FindOne(x => x.descripcion_estado_item == estado.descripcion_estado_item);
            if (existeEstado == null)
            {
                estado.Id = estado.GetInternalId("ESI");
                await _unitOfWork.Service_Commands<Orden_Item_Ref_Estados>().InsertOneAsync(estado);
            }
            else
            {
                throw new Exception("Codigo de Estado ya existe");
            }


            return estado;
        }

        public IQueryable<Orden_Item_Ref_Estados> ObtenerEstados(IUnitOfWork<Orden_Item_Ref_Estados> _unitOfWork)
        {
            return _unitOfWork.Service_Queries<Orden_Item_Ref_Estados>().AsQueryable();
        }

        public Orden_Item_Ref_Estados ObtenerEstadosPorCodigo(string codigo, IUnitOfWork<Orden_Item_Ref_Estados> _unitOfWork)
        {
            return _unitOfWork.Service_Queries<Orden_Item_Ref_Estados>().FindOne(x => x.codigo_estado_item == codigo);
        }
    }
}
