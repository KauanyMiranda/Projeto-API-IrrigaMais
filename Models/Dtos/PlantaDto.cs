using System.ComponentModel.DataAnnotations;

namespace Projeto_IrrigaMais_API.Models.Dtos
{
    public class PlantaDto
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "A necessidade hídrica é obrigatória")]
        public int NecessidadeHidricaId { get; set; }

        [Required(ErrorMessage = "O sensor é obrigatório")]
        public int SensorId { get; set; }

        [Required(ErrorMessage = "O usuário é obrigatório")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "A rotina é obrigatória")]
        public int RotinaId { get; set; }
    }
}
