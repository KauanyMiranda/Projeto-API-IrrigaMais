using System.ComponentModel.DataAnnotations.Schema;

namespace ApiServico.Models
{  
      [Table("rotinas")]
        public class Rotina
        {
            [Column("id_rotina")]
            public int Id { get; set; }

            [Column("tipo_execucao")]
            public string tipo_execucao { get; set; }

            [Column("horario")]
            public DateTime horario { get; set; } = DateTime.Now;

            [Column("frequencia")]
            public int frequencia { get; set; }

            [Column("nome_rotina")]
            public string nome_rotina { get; set; }

            [Column("dia_seg")]
            public bool dia_seg { get; set; }

            [Column("dia_ter")]
            public bool dia_ter { get; set; }

            [Column("dia_qua")]
            public bool dia_qua { get; set; }

            [Column("dia_qui")]
            public bool dia_qui { get; set; }

            [Column("dia_sex")]
            public bool dia_sex { get; set; }

            [Column("dia_sab")]
            public bool dia_sab { get; set; }

            [Column("dia_dom")]
            public bool dia_dom { get; set; }
       
    }
}
