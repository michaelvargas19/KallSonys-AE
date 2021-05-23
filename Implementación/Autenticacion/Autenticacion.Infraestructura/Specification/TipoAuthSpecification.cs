using Autenticacion.Dominio.Specification;
using Autenticacion.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Infraestructura.Specification
{
    public class TipoAuthSpecification : BaseSpecification<TipoAutenticacion>
    {
        public TipoAuthSpecification() : base()
        {
            
        }

        public TipoAuthSpecification(int id) : base(x => x.IdTipo == id)
        {
        }
    }
}
