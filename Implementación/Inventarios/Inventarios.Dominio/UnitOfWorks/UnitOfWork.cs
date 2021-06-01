using Inventarios.Dominio.IUnitOfWorks;
using Inventarios.Infraestructura.Entities;
using Inventarios.Infraestructura.IRepositories;
using Inventarios.Infraestructura.Repository;

namespace Inventarios.Infraestructura.UnitOfWorks
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