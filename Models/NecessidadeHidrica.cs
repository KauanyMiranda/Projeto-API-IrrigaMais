using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Projeto_IrrigaMais_API.Models
{
    [Table("necessidade_hidrica")]
    public class NecessidadeHidrica
    {
        [Column("id_necessidade_hidrica")]
        public int Id { get; set; }

        [Column("nome")]
        public string? Nome { get; set; }

        [Column("qtd_litros")]
        public double qtdLitros { get; set; }

        [JsonIgnore]
        public ICollection<Planta> Plantas { get; set; } = new List<Planta>();
    }
}
