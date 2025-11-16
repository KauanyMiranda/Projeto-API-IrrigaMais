using Projeto_IrrigaMais_API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiServico.Models
{
    [Table("usuarios")]   
    public class Usuario
    {
        [Column("id_usuario")]
        public int Id { get; set; }

        [Column("nome_usuario")]
        public string? Nome { get; set; }

        [Column("email_usuario")]
        public string? Email { get; set; }

        
        [Column("senha_usuario")]
        public string? Senha { get; set; }

        [JsonIgnore]
        public ICollection<Planta> Planta { get; set; } = [];
    }
}
