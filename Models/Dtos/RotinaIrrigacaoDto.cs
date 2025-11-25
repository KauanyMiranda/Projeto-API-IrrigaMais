using System.ComponentModel.DataAnnotations;

namespace Projeto_IrrigaMais_API.Models.Dtos
{
    public class RotinaIrrigacaoDto
    {
        [Required]
        [MinLength(1)]
        public List<int> RotinasIds { get; set; } = [];
    }
}
