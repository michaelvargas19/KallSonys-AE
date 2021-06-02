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
    public class EstadosOrdenController : ControllerBase
    {

        private readonly IUnitOfWork<Orden_Ref_Estados> _unitOfWork;
        private readonly Service_EstadosOrden_command estadoOrden;



        public EstadosOrdenController(IUnitOfWork<Orden_Ref_Estados> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<ActionResult<Orden_Ref_Estados>> AgregarEstadoOrden(Orden_Ref_Estados estado)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var actualiza = await estadoOrden.AgregarEstadoOrden(estado, _unitOfWork);
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
        public IQueryable<Orden_Ref_Estados> ObtenerEstados()
        {
            try
            {
                var lstestado = estadoOrden.ObtenerEstados(_unitOfWork);
                return lstestado;
            }
            catch (Exception error)
            {
                throw new Exception(error.InnerException + error.Message);
            }
        }

        [Route("[action]/{codigo}")]

        [HttpGet]
        public ActionResult<Orden_Ref_Estados> ObtenerEstadosPorCodigo(string codigo)
        {
            try
            {
                var lstestado = estadoOrden.ObtenerEstadosPorCodigo(codigo,_unitOfWork);
                return lstestado;
            }
            catch (Exception error)
            {
                throw new Exception(error.InnerException + error.Message);
            }
        }
    }
}
