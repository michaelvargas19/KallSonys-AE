using Autenticacion.Dominio.Specification;
using Autenticacion.Infraestructura.Entities;
using Autenticacion.Infraestructura.ISpecification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Infraestructura.Specification
{
    public class AplicacionSpecification : BaseSpecification<Aplicacion>
    {
        public AplicacionSpecification() : base()
        {
            AddInclude(a=> a.Roles);
        }

        public AplicacionSpecification(string idAplicacion) : base(x => x.IdAplicacion == idAplicacion)
        {
            AddInclude(a => a.Roles);
        }

        public AplicacionSpecification(string idAplicacion, bool estado) : base(x => x.IdAplicacion == idAplicacion && x.Estado == estado)
        {
            AddInclude(a => a.Roles);
        }

        public AplicacionSpecification(string idAplicacion, bool estado, bool PermiteJWT) : base(x => x.IdAplicacion == idAplicacion &&
                                                                                                      x.Estado == estado             &&
                                                                                                      x.PermiteJWT == PermiteJWT)
        {
            AddInclude(a => a.Roles);
        }
    }
}
