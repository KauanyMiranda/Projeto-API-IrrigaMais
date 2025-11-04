using System.ComponentModel.DataAnnotations;

namespace Projeto_IrrigaMais_API.Models.Dtos
{
    public class PlantaDto
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public required string Nome { get; set; }

        [Required]
        public int NecessidadeHidricaId { get; set; }
    }
}
