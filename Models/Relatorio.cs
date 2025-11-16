using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto_IrrigaMais_API.Models
{
    [Table("relatorios")]
    public class Relatorio
    {
        [Column("id_relatorio")]
        public int Id { get; set; }

        [Column("tipo_relatorio")]
        public string tipoRelatorio { get; set; }

        [Column("dt_geracao")]
        public DateTime dtGeracao { get; set; } = DateTime.Now;

        [Column("dt_inicial")]
        public DateTime? DataInicial { get; set; }

        [Column("dt_final")]
        public DateTime? DataFinal { get; set; }


    }
}
