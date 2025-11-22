using System.ComponentModel.DataAnnotations;

namespace Projeto_IrrigaMais_API.Models.Dtos
{
    public class ApiDto
    {
        [Required(ErrorMessage = "A cidade é obrigatória")]
        public required string cidade { get; set; }

        [Required(ErrorMessage = "O país é obrigatório")]
        public required string pais { get; set; }

        public string? descricao { get; set; }

        public string? icone { get; set; }

        [Required(ErrorMessage = "A temperatura máxima é obrigatória")]
        public double temp_max { get; set; }

        [Required(ErrorMessage = "A temperatura mínima é obrigatória")]
        public double temp_min { get; set; }

        [Required(ErrorMessage = "A previsão é obrigatória")]
        public double previsao { get; set; }

        [Required(ErrorMessage = "A umidade é obrigatória")]
        public double umidade { get; set; }

        [Required(ErrorMessage = "A velocidade do vento é obrigatória")]
        public double vento { get; set; }

        public DateTime DtConsulta { get; set; } = DateTime.Now;
    }
}
