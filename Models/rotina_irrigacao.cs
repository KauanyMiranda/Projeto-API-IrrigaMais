using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto_IrrigaMais_API.Models
{
    [Table("rotina_irrigacao")]
    public class rotina_irrigacao
    {
        [Column("id_rotina_irrigacao")]
        public int id { get; set; }
        [Column("fk_id_rotina")]
        public int fk_id_rotina { get; set; }
        [Column("fk_id_irrigacao")]
        public int fk_id_irrigacao { get; set; }
    }
}
