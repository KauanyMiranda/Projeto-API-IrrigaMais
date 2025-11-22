using System.ComponentModel.DataAnnotations;

namespace Projeto_IrrigaMais_API.Models.Dtos
{
    public class IrrigacaoDto
    {
        [Required(ErrorMessage = "O modo de irrigação é obrigatório")]
        public required string modo_irrigacao { get; set; }

        [Required(ErrorMessage = "O consumo hídrico é obrigatório")]
        public double consumo_hidrico { get; set; }

        [Required(ErrorMessage = "A data inicial é obrigatória")]
        public DateTime DataInicial { get; set; }

        [Required(ErrorMessage = "A data final é obrigatória")]
        public DateTime DataFinal { get; set; }

        [Required(ErrorMessage = "O id da leitura do sensor é obrigatório")]
        public int fk_id_leitura_sensor { get; set; }

        [Required(ErrorMessage = "O id da API é obrigatório")]
        public int fk_id_api { get; set; }
    }
}
