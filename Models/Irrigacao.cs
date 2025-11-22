using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Projeto_IrrigaMais_API.Models
{
    [Table("Irrigacao")]
    public class Irrigacao
    {
        [Column("id_irrigacao")]
        public int id { get; set; }

        [Column("modo_irrigacao")]
        public string modo_irrigacao { get; set; }

        [Column("consumo_hidrico")]
        public double consumo_hidrico { get; set; }

        [Column("dt_inicial")]
        public DateTime dt_inicial { get; set; } = DateTime.Now;

        [Column("dt_final")]
        public DateTime dt_final { get; set; } = DateTime.Now;

        [JsonIgnore]
        [Column("fk_id_leitura_sensor")]
        public int fk_id_leitura_sensor { get; set; }

        [JsonIgnore]
        [Column("fk_id_api")]
        public int fk_id_api { get; set; }

    }
}
