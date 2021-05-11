using Catalogos.Dominio.IUnitOfWorks;
using Catalogos.Dominio.Modelo;
using Catalogos.Infraestructura.Specification;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogosController : ControllerBase
    {
        //private readonly IMongoRepository<Catalogo> _catalogoRepository;
        private readonly IUnitOfWork<Catalogo> _unitOfWork;

        //public CatalogosController(IMongoRepository<Catalogo> catalogoRepository)
        public CatalogosController(IUnitOfWork<Catalogo> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("")]
        public IEnumerable<Catalogo> VerCatalogos()
        {
            return _unitOfWork.Repository<Catalogo>().AsQueryable().AsEnumerable();
            
        }

        [HttpGet("paginacion")]
        public IEnumerable<Catalogo> verPaginacion(int skip, int take)
        {
            return _unitOfWork.Repository<Catalogo>().Find(new PagingSpecification<Catalogo>(skip, take)).ToList();
        }

        [HttpGet("ranking")]
        public IEnumerable<Catalogo> verRanking(int skip, int take)
        {
            return _unitOfWork.Repository<Catalogo>().Find(new PagingSpecification<Catalogo>(skip, take, c=> c.Id, false)).ToList();
        }


        [HttpPost("")]
        public async Task AgregarCatalogo(Catalogo catalogo)
        {
            await _unitOfWork.Repository<Catalogo>().InsertOneAsync(catalogo);
        }



        
    }
}
