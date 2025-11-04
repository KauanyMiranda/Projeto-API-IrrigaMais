using System.ComponentModel.DataAnnotations;

namespace Projeto_IrrigaMais_API.Models.Dtos
{
    public class SensorDto
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "A localização é obrigatória")]
        public required string Localizacao { get; set; }

        [Required]
        public int TipoSensorId { get; set; }
    }
}
