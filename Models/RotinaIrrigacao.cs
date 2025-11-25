using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Projeto_IrrigaMais_API.Models
{
    [Table("rotina_irrigacao")]
    public class RotinaIrrigacao
    {
        [Column("id_rotina_irrigacao")]
        public int Id { get; set; }

        public virtual Rotina? Rotina { get; set; }

        [Column("fk_rotina_id")]
        [JsonIgnore]
        public int RotinaId { get; set; }

        public virtual Irrigacao? Irrigacao { get; set; }
      
        [Column("fk_irrigacao_id")]
        [JsonIgnore]
        public int IrrigacaoId { get; set; }
    }
}
