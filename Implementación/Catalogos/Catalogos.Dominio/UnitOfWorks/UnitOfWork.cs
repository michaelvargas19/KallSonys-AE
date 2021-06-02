using Catalogos.Dominio.IUnitOfWorks;
using Catalogos.Infraestructura.Entities;
using Catalogos.Infraestructura.Integracion.Proveedores;
using Catalogos.Infraestructura.IRepositories;
using Catalogos.Infraestructura.Repository;
using Catalogos.Infraestructura.Util;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Catalogos.Infraestructura.UnitOfWorks
{

    public class UnitOfWork<T> : IUnitOfWork<T> where T : IDocument
    {
        private readonly IMongoContext _context;
        private readonly IMongoRepository<T> _repository;
        protected IIntegrationProveedores _integrationProveedores;
        private readonly IConfiguration _configuration;
        private readonly IUtilsInfra _util;

        public UnitOfWork(IMongoContext context,
                          IMongoRepository<T> repository,
                          IConfiguration configuration,
                          IUtilsInfra util)
        {
            _context = context;
            _repository = repository;
            this._configuration = configuration;
            this._util = util;
        }

        public IMongoRepository<T> Repository<T>() where T : IDocument
        {
            return (IMongoRepository<T>)_repository;
        }

        public bool Commit()
        {
            int changeAmount = _context.SaveChanges().Result;

            return changeAmount > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IIntegrationProveedores IIntegrationProveedores()
        {
            if(_integrationProveedores == null)
            {
                this._integrationProveedores = new IntegrationProveedores(this._configuration, this._util);
            }
            
            return _integrationProveedores;
        }
    }
}