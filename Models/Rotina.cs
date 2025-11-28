using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Projeto_IrrigaMais_API.Models
{
    [Table("rotina")]
    public class Rotina
    {
        [Column("id_rotina")]
        public int Id { get; set; }

        [Column("nome_rotina")]
        public required string NomeRotina { get; set; }

        [Column("tipo_execucao")]
        public required string TipoExecucao { get; set; }

        [Column("horario")]
        public required string Horario { get; set; }

        [Column("frequencia")]
        public required int Frequencia { get; set; }

        [Column("dia_seg")]
        public bool DiaSeg { get; set; }

        [Column("dia_ter")]
        public bool DiaTer { get; set; }

        [Column("dia_qua")]
        public bool DiaQua { get; set; }

        [Column("dia_qui")]
        public bool DiaQui { get; set; }

        [Column("dia_sex")]
        public bool DiaSex { get; set; }

        [Column("dia_sab")]
        public bool DiaSab { get; set; }

        [Column("dia_dom")]
        public bool DiaDom { get; set; }
        public virtual Usuario? Usuario { get; set; }

        [JsonIgnore]
        [Column("id_usuario_fk")]
        public int UsuarioId { get; set; }

        public ICollection<Planta> Plantas { get; set; } = new List<Planta>();
        public ICollection<RotinaIrrigacao> RotinasIrrigacoes { get; set; } = [];

    }
}
