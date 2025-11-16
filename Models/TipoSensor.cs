using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Projeto_IrrigaMais_API.Models
{
    [Table("tipo_sensor")]
    public class TipoSensor
    {
        [Column("id_tipo_sensor")]
        public int Id { get; set; }

        [Column("nome")]
        public string? Nome { get; set; }

        [Column("unidade_medida")]
        public string? UnidadeMedida { get; set; }

        [JsonIgnore]
        public ICollection<Sensor> Sensores { get; set; } = new List<Sensor>();

    }
}
