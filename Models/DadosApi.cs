using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto_IrrigaMais_API.Models
{
    [Table("dados_api")]
    public class DadosApi
    {
        [Column("id_dados_api")]
        public int Id { get; set; }

        [Column("cidade")]
        public required string Cidade { get; set; }

        [Column("pais")]
        public required string Pais { get; set; }

        [Column("descricao")]
        public string? Descricao { get; set; }

        [Column("icone")]
        public string? Icone { get; set; }

        [Column("temp")]
        public required double Temp { get; set; }

        [Column("temp_max")]
        public required double TempMax { get; set; }

        [Column("temp_min")]
        public required double TempMin { get; set; }

        [Column("previsao")]
        public required double Previsao { get; set; }

        [Column("umidade")]
        public required double Umidade { get; set; }

        [Column("vento")]
        public required double Vento { get; set; }

        [Column("dt_consulta")]
        public DateTime DtConsulta { get; set; } = DateTime.Now;

        public ICollection<Irrigacao> Irrigacao { get; set; } = new List<Irrigacao>();
    }
}
