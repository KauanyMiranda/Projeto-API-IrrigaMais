using Projeto_IrrigaMais_API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Projeto_IrrigaMais_API.Models
{
    [Table("leitura_sensor")]
    public class LeituraSensor
    {
        [Column("id_leitura_sensor")]
        public int Id { get; set; }

        [Column("dt_leitura")]
        public DateTime DtLeitura { get; set; } = DateTime.Now;

        [Column("valor")]
        public double Valor { get; set; }

        public virtual Sensor? Sensor { get; set; }

        [JsonIgnore]
        [Column("id_sensor_fk")]
        public int SensorId { get; set; }
    }
}
