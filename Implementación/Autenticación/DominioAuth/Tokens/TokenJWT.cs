using System.ComponentModel.DataAnnotations;

namespace DominioAuth.Tokens
{
    public class TokenJWT
    {
        [Required]
        [MaxLength(450)]
        public string IdAplicacion { get; set; }

        [Required]
        public string Token { get; set; }

        public bool TokenValido { get; set; }
    }
}
