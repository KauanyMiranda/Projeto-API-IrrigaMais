using System.ComponentModel.DataAnnotations;

namespace Projeto_IrrigaMais_API.Models.Dtos
{
    public class RelatorioDto
    {
        [Required(ErrorMessage = "O tipo de relatório é obrigatório")]
        public required string TipoRelatorio { get; set; }

        [Required(ErrorMessage = "A data inicial é obrigatória")]
        public required DateTime DataInicial { get; set; }

        [Required(ErrorMessage = "A data final é obrigatória")]
        public required DateTime DataFinal { get; set; }

        [Required(ErrorMessage = "O usuário é obrigatório")]
        public int UsuarioId { get; set; }
    }
}
