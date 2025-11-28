using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Projeto_IrrigaMais_API.Models
{
    [Table("planta")]
    public class Planta
    {
        [Column("id_planta")]
        public int Id { get; set; }

        [Column("nome_planta")]
        public required string Nome { get; set; }

        public virtual NecessidadeHidrica? NecessidadeHidrica { get; set; }
        public virtual Rotina? Rotina { get; set; }
        public virtual Usuario? Usuario { get; set; }
        public virtual Sensor? Sensor { get; set; }

        [JsonIgnore]
        [Column("id_necessidade_hidrica_fk")]
        public int NecessidadeHidricaId { get; set; }

        [JsonIgnore]
        [Column("id_usuario_fk")]
        public int UsuarioId { get; set; }

        [JsonIgnore]
        [Column("id_sensor_fk")]
        public int SensorId { get; set; }

        [JsonIgnore]
        [Column("id_rotina_fk")]
        public int RotinaId { get; set; }
    }
}
