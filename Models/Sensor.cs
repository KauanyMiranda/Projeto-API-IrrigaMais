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
        public string StatusSensor { get; set; } = "Ativo";

        public virtual TipoSensor? TipoSensor { get; set; }
        public virtual Usuario? Usuario { get; set; }

        [JsonIgnore]
        [Column("id_tipo_sensor_fk")]
        public int TipoSensorId { get; set; }

        [JsonIgnore]
        [Column("id_usuario_fk")]
        public int UsuarioId { get; set; }
    }
}