using Autenticacion.Dominio.Specification;
using Autenticacion.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Infraestructura.Specification
{
    public class UsuarioSpecification : BaseSpecification<Usuario>
    {
        public UsuarioSpecification() : base()
        {
        }

        public UsuarioSpecification(int idUsuario) : base(x => x.Id == idUsuario)
        {

        }

        public UsuarioSpecification(string usuario) : base(x => x.NormalizedUserName == usuario.ToUpper() )
        {

        }

        public UsuarioSpecification(string usuario, string email) : base(x => x.NormalizedUserName == usuario.ToUpper() || x.NormalizedEmail == email.ToUpper())
        {
        }

    }
}
