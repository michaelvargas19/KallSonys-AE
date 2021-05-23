using Autenticacion.Dominio.Specification;
using Autenticacion.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Infraestructura.Specification
{
    public class RolSpecification : BaseSpecification<Rol>
    {
        public RolSpecification() : base()
        {
            
        }

        public RolSpecification(string idAplicacion) : base(x => x.IdAplicacion == idAplicacion)
        {
        }

        public RolSpecification(int idRol) : base(x => x.Id == idRol)
        {
        }


    }
}
