using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Projeto_IrrigaMais_API.Models
{
    [Table("sensor")]
    public class Sensor
    {
        [Column("id_sensor")]
        public int Id { get; set; }

        [Column("nome")]
        public required string Nome { get; set; }

        [Column("localizacao")]
        public required string Localizacao { get; set; }

        [Column("status_sensor")]
        public string Status { get; set; } = "Ativo";

        public virtual TipoSensor? TipoSensor { get; set; }

        [JsonIgnore]
        [Column("fk_tipo_sensor_id")]
        public int TipoSensorId { get; set; }
    }
}
