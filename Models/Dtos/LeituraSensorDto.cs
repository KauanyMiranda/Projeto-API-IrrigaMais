using System.ComponentModel.DataAnnotations;

namespace Projeto_IrrigaMais_API.Models.Dtos
{
    public class LeituraSensorDto
    {
        [Required(ErrorMessage = "O valor da leitura é obrigatório")]
        public required double Valor  { get; set; }

        public int SensorId { get; set; }

    }
}
