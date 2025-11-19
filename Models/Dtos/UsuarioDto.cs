using System.ComponentModel.DataAnnotations;

namespace Projeto_IrrigaMais_API.Models.Dtos
{
    public class UsuarioDto
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        public required string Senha { get; set; }
    }
}
