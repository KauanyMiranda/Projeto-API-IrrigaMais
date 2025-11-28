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
        public virtual DadosApi? Api { get; set; }

        [JsonIgnore]
        [Column("id_leitura_sensor_fk")]
        public int LeituraSensorId { get; set; }

        [JsonIgnore]
        [Column("id_dados_api_fk")]
        public int DadosApiId { get; set; }

        public ICollection<RotinaIrrigacao> RotinasIrrigacoes { get; set; } = [];
    }
}
