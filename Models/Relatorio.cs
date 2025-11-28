using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Projeto_IrrigaMais_API.Models
{
    [Table("relatorio")]
    public class Relatorio
    {
        [Column("id_relatorio")]
        public int Id { get; set; }

        [Column("tipo_relatorio")]
        public required string tipoRelatorio { get; set; }

        [Column("dt_geracao")]
        public DateTime dtGeracao { get; set; } = DateTime.Now;

        [Column("dt_inicial")]
        public required DateTime DataInicial { get; set; }

        [Column("dt_final")]
        public required DateTime DataFinal { get; set; }

        public virtual Usuario? Usuario { get; set; }

        [JsonIgnore]
        [Column("id_usuario_fk")]
        public int UsuarioId { get; set; }

    }
}
