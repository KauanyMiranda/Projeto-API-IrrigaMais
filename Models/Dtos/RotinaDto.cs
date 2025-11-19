using System.ComponentModel.DataAnnotations;

namespace Projeto_IrrigaMais_API.Models.Dtos
{
    public class RotinaDto
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public required string nome_rotina { get; set; }

        [Required(ErrorMessage = "O tipo de execução é obrigatório")]
        public required string tipo_execucao { get; set; }

        [Required(ErrorMessage = "O horário é obrigatório")]
        public required string horario { get; set; }

        [Required(ErrorMessage = "A frequência é obrigatória")]
        public required int frequencia { get; set; }
        public bool dia_seg { get; set; }
        public bool dia_ter { get; set; }
        public bool dia_qua { get; set; }
        public bool dia_qui { get; set; }
        public bool dia_sex { get; set; }
        public bool dia_sab { get; set; }
        public bool dia_dom { get; set; }
    }
}
