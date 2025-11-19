using System.ComponentModel.DataAnnotations;

namespace Projeto_IrrigaMais_API.Models.Dtos
{
    public class NecessidadeHidricaDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "A quantidade de litros é obrigatória.")]
        public required double qtdlitros { get; set; }
    }
}
