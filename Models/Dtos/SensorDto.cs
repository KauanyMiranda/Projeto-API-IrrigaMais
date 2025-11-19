using System.ComponentModel.DataAnnotations;

namespace Projeto_IrrigaMais_API.Models.Dtos
{
    public class SensorDto
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "A localização é obrigatória")]
        public required string Localizacao { get; set; }

        [Required(ErrorMessage = "O tipo do sensor é obrigatório")]
        public int TipoSensorId { get; set; }

        [Required(ErrorMessage = "O usuário é obrigatório")]
        public int UsuarioId { get; set; }
    }
}
