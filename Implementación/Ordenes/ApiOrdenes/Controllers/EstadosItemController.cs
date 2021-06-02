using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ordenes.Dominio.IUnitOfWorks;
using Ordenes.Dominio.Models;
using Ordenes.Infraestructura.Services;

namespace ApiOrdenes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadosItemController : ControllerBase
    {
        private readonly IUnitOfWork<Orden_Item_Ref_Estados> _unitOfWork;
        private readonly Services_EstadosItem_command estadoItem;


        public EstadosItemController(IUnitOfWork<Orden_Item_Ref_Estados> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<ActionResult<Orden_Item_Ref_Estados>> AgregarEstadoOrden(Orden_Item_Ref_Estados estado)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var actualiza = await estadoItem.AgregarEstadoOrden(estado, _unitOfWork);
                    return Ok(actualiza);
                }
                else
                {
                    return BadRequest("valide los parametros");
                }
            }
            catch (Exception error)
            {
                throw new Exception(error.InnerException + error.Message);
            }

        }

        [HttpGet]
        public IQueryable<Orden_Item_Ref_Estados> ObtenerEstados()
        {
            try
            {
                var lstestado = estadoItem.ObtenerEstados(_unitOfWork);
                return lstestado;
            }
            catch (Exception error)
            {
                throw new Exception(error.InnerException + error.Message);
            }
        }

        [Route("[action]/{codigo}")]

        [HttpGet]
        public ActionResult<Orden_Item_Ref_Estados> ObtenerEstadosPorCodigo(string codigo)
        {
            try
            {
                var lstestado = estadoItem.ObtenerEstadosPorCodigo(codigo, _unitOfWork);
                return lstestado;
            }
            catch (Exception error)
            {
                throw new Exception(error.InnerException + error.Message);
            }
        }

    }
}
