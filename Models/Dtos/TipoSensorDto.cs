using System.ComponentModel.DataAnnotations;

namespace Projeto_IrrigaMais_API.Models.Dtos
{
    public class TipoSensorDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "A unidade de medida é obrigatória.")]
        public required string UnidadeMedida { get; set; }
    }
}
