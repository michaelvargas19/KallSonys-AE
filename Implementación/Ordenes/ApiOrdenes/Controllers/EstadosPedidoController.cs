using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ordenes.Dominio.IUnitOfWorks;
using Ordenes.Dominio.Models;

namespace ApiOrdenes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadosPedidoController : ControllerBase
    { 
        private readonly IUnitOfWork<Pedido_Ref_Estados> _unitOfWork;



        public EstadosPedidoController(IUnitOfWork<Pedido_Ref_Estados> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

     


    }
}
