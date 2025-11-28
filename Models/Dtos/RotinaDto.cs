using System.ComponentModel.DataAnnotations;

namespace Projeto_IrrigaMais_API.Models.Dtos
{
    public class RotinaDto
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public required string NomeRotina { get; set; }

        [Required(ErrorMessage = "O tipo de execução é obrigatório")]
        public required string TipoExecucao { get; set; }

        [Required(ErrorMessage = "O horário é obrigatório")]
        public required string Horario { get; set; }

        [Required(ErrorMessage = "A frequência é obrigatória")]
        public required int Frequencia { get; set; }
        public bool DiaSeg { get; set; }
        public bool DiaTer { get; set; }
        public bool DiaQua { get; set; }
        public bool DiaQui { get; set; }
        public bool DiaSex { get; set; }
        public bool DiaSab { get; set; }
        public bool DiaDom { get; set; }

        [Required(ErrorMessage = "O usuário é obrigatório")]
        public int UsuarioId { get; set; }
    }
}
