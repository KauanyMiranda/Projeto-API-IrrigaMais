using System.ComponentModel.DataAnnotations;

namespace Projeto_IrrigaMais_API.Models.Dtos
{
    public class IrrigacaoDto
    {

        [Required(ErrorMessage = "O consumo hídrico é obrigatório")]
        public required double ConsumoHidrico { get; set; }

        [Required(ErrorMessage = "A data inicial é obrigatória")]
        public required DateTime DataInicial { get; set; }

        [Required(ErrorMessage = "A data final é obrigatória")]
        public required DateTime DataFinal { get; set; }

        [Required(ErrorMessage = "O ID da leitura do sensor é obrigatório")]
        public int LeituraSensorId { get; set; }

        [Required(ErrorMessage = "O ID dos Dados da API é obrigatório")]
        public int DadosApiId { get; set; }
    }
}
