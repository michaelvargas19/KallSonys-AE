using Catalogos.Dominio.IUnitOfWorks;
using Catalogos.Infraestructura.Entities;
using Catalogos.Infraestructura.IRepositories;
using Catalogos.Infraestructura.Repository;
using System.Threading.Tasks;

namespace Catalogos.Infraestructura.UnitOfWorks
{

    public class UnitOfWork<T> : IUnitOfWork<T> where T : IDocument
    {
        private readonly IMongoContext _context;
        private readonly IMongoRepository<T> _repository;
        public UnitOfWork(IMongoContext context,
                          IMongoRepository<T> repository)
        {
            _context = context;
            _repository = repository;
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

        
    }
}