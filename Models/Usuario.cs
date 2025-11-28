using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Projeto_IrrigaMais_API.Models
{
    [Table("usuario")]
    public class Usuario
    {
        [Column("id_usuario")]
        public int Id { get; set; }

        [Column("nome_usuario")]
        public required string Nome { get; set; }

        [Column("email_usuario")]
        public required string Email { get; set; }


        [Column("senha_usuario")]
        public required string Senha { get; set; }

        [JsonIgnore]
        public ICollection<Planta> Plantas { get; set; } = new List<Planta>();

        [JsonIgnore]
        public ICollection<Sensor> Sensores { get; set; } = new List<Sensor>();

        [JsonIgnore]
        public ICollection<Rotina> Rotinas { get; set; } = new List<Rotina>();
    }
}