using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Projeto_IrrigaMais_API.Models
{
    [Table("irrigacao")]
    public class Irrigacao
    {
        [Column("id_irrigacao")]
        public int Id { get; set; }

        [Column("consumo_hidrico")]
        public required double ConsumoHidrico { get; set; }

        [Column("dt_inicial")]
        public DateTime DtInicial { get; set; } = DateTime.Now;

        [Column("dt_final")]
        public DateTime DtFinal { get; set; } = DateTime.Now;

        public virtual LeituraSensor? LeituraSensor { get; set; }
        public virtual Api? Api { get; set; }

        [JsonIgnore]
        [Column("fk_leitura_sensor_id")]
        public int LeituraSensorId { get; set; }

        [JsonIgnore]
        [Column("fk_api_id")]
        public int ApiId { get; set; }

        public ICollection<RotinaIrrigacao> RotinasIrrigacoes { get; set; } = [];
    }
}
