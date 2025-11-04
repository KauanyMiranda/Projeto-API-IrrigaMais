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

        [JsonIgnore]
        [Column("fk_necessidade_hidrica_id")]
        public int NecessidadeHidricaId { get; set; }
    }
}
