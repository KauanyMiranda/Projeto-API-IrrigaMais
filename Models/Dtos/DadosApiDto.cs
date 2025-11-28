using System.ComponentModel.DataAnnotations;

namespace Projeto_IrrigaMais_API.Models.Dtos
{
    public class DadosApiDto
    {
        [Required(ErrorMessage = "A cidade é obrigatória")]
        public required string Cidade { get; set; }

        [Required(ErrorMessage = "O país é obrigatório")]
        public required string Pais { get; set; }
        public string? Descricao { get; set; }
        public string? Icone { get; set; }

        [Required(ErrorMessage = "A temperatura é atual obrigatória")]
        public required double Temp { get; set; }

        [Required(ErrorMessage = "A temperatura máxima é obrigatória")]
        public double TempMax { get; set; }

        [Required(ErrorMessage = "A temperatura mínima é obrigatória")]
        public double TempMin { get; set; }

        [Required(ErrorMessage = "A previsão é obrigatória")]
        public double Previsao { get; set; }

        [Required(ErrorMessage = "A umidade é obrigatória")]
        public double Umidade { get; set; }

        [Required(ErrorMessage = "A velocidade do vento é obrigatória")]
        public double Vento { get; set; }
    }
}
