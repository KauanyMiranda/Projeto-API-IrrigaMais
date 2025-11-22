using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto_IrrigaMais_API.Models
{
    [Table("Api")]
    public class Api
    {
        [Column("id_api")]
        public int Id { get; set; }

        [Column("cidade")]
        public string cidade {get; set;}

        [Column("pais")]
        public string pais { get; set;}

        [Column("descricao")]
        public string descricao {get; set;}

        [Column("icone")]
        public string icone { get; set;}

        [Column("temp_max")]
        public double temp_max { get; set;}

        [Column("temp_min")]
        public double temp_min { get; set;}

        [Column("previsao")]
        public double previsao { get; set;}

        [Column("umidade")]
        public double umidade { get; set;}

        [Column("vento")]
        public double vento { get; set;}

        [Column("dt_consulta")]
        public DateTime dt_consulta { get; set;} = DateTime.Now;

        public ICollection<Irrigacao> Irrigacao { get; set; } = new List<Irrigacao>();
    }
}
