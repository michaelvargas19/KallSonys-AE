using Microsoft.AspNetCore.Mvc;
using NegocioAuth;


namespace AutenticaciónAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly INegocioWrapper negocio;

        public UsuariosController(INegocioWrapper negocioWrapper)
        {
            this.negocio = negocioWrapper;
        }


    }
}
