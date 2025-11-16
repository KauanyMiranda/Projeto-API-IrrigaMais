using Projeto_IrrigaMais_API.Models;
using Microsoft.EntityFrameworkCore;
using ApiServico.Models;

namespace Projeto_IrrigaMais_API.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Planta> Plantas { get; set; }
        public DbSet<Sensor> Sensores { get; set; }
        public DbSet<NecessidadeHidrica> necessidadesHidricas { get; set; }
        public DbSet<TipoSensor> TipoSensores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rotina> Rotinas { get; set; }
        public DbSet<Relatorio> Relatorios { get; set; }
        

    }
}
