using Projeto_IrrigaMais_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Projeto_IrrigaMais_API.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Planta> Plantas { get; set; }
        public DbSet<Sensor> Sensores { get; set; }
        public DbSet<Rotina> Rotinas { get; set; }
        public DbSet<NecessidadeHidrica> necessidadesHidricas { get; set; }
        public DbSet<TipoSensor> TipoSensores { get; set; }
        public DbSet<LeituraSensor> LeiturasSensores { get; set; }
        public DbSet<Relatorio> Relatorios { get; set; }
        public DbSet<Irrigacao> Irrigacoes { get; set; }
        public DbSet<Api> Apis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}
