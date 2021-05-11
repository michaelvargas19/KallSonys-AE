using Catalogos.Dominio.IRepositories;
using Catalogos.Dominio.ISpecification;
using Catalogos.Dominio.IUnitOfWorks;
using Catalogos.Dominio.Modelo;
using Catalogos.Dominio.Repository;
using Catalogos.Infraestructura.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<bool> Commit()
        {
            var changeAmount = await _context.SaveChanges();

            return changeAmount > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        
    }
}