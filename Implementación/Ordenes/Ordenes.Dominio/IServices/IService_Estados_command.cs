using Ordenes.Dominio.IUnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordenes.Dominio.IServices
{
    public interface IService_Estados_command<T>
    {
        IQueryable<T> ObtenerEstados(IUnitOfWork<T> _unitOfWork);

        Task<T> AgregarEstadoOrden(T estado, IUnitOfWork<T> _unitOfWork);
        T ObtenerEstadosPorCodigo(string codigo, IUnitOfWork<T> _unitOfWork);
    }
}
