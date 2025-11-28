using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_IrrigaMais_API.DataContext;
using Projeto_IrrigaMais_API.Models;
using Projeto_IrrigaMais_API.Models.Dtos;
using System.Threading.Tasks;

namespace Projeto_IrrigaMais_API.Controllers
{
    [Route("/planta")]
    [ApiController]
    public class PlantaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlantaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos(
            [FromQuery] string? buscar
            )
        {
            var query = _context.Plantas.AsQueryable();

            if (buscar is not null)
            {
                query = query.Where(x => x.Nome.Contains(buscar));

                return Ok(query);
            }

            var plantas = await query
                .Include(n => n.NecessidadeHidrica)
                .Include(r => r.Rotina)
                .Include(s => s.Sensor)
                .Include(u => u.Usuario)
                .Select(p => new
                {
                    p.Id,
                    p.Nome,
                    NecessidadeHidrica = new { p.NecessidadeHidrica.Nome },
                    Rotina = new { 
                        p.Rotina.NomeRotina,
                        p.Rotina.TipoExecucao,
                        p.Rotina.Horario,
                        p.Rotina.Frequencia,
                        p.Rotina.DiaSeg,
                        p.Rotina.DiaTer,
                        p.Rotina.DiaQua,
                        p.Rotina.DiaQui,
                        p.Rotina.DiaSex,
                        p.Rotina.DiaSab,
                        p.Rotina.DiaDom
                    },
                    Sensor = new { p.Sensor.Nome },
                    Usuario = new { p.Usuario.Nome }
                })
                .ToListAsync();

            return Ok(plantas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var planta = await _context.Plantas
                .Include(n => n.NecessidadeHidrica)
                .Include(s => s.Sensor)
                .Include(u => u.Usuario)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (planta is null)
            {
                return NotFound();
            }

            return Ok(planta);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] PlantaDto novaPlanta)
        {
            var necessidadeHidrica = await _context
                .necessidadesHidricas
                .FirstOrDefaultAsync(x => x.Id == novaPlanta.NecessidadeHidricaId);

            var sensor = await _context
                .Sensores
                .FirstOrDefaultAsync(x => x.Id == novaPlanta.SensorId);

            var usuario = await _context
                .Usuarios
                .FirstOrDefaultAsync(x => x.Id == novaPlanta.UsuarioId);
            var rotina = await _context
                .Rotinas
                .FirstOrDefaultAsync(x => x.Id == novaPlanta.RotinaId);

            if (necessidadeHidrica is null)
            {
                return NotFound("Necessidade Hídrica não encontrada");
            }

            if (sensor is null)
            {
                return NotFound("Sensor não encontrado");
            }

            if (usuario is null)
            {
                return NotFound("Usuário não encontrado");
            }

            if (rotina is null)
            {
                return NotFound("Rotina não encontrada");
            }

            var planta = new Planta()
            {
                Nome = novaPlanta.Nome,
                NecessidadeHidricaId = novaPlanta.NecessidadeHidricaId,
                RotinaId = novaPlanta.RotinaId,
                SensorId = novaPlanta.SensorId,
                UsuarioId = novaPlanta.UsuarioId
            };

            await _context.Plantas.AddAsync(planta);
            await _context.SaveChangesAsync();

            return Created("", planta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] PlantaDto atualizarPlanta)
        {
            var planta = await _context.Plantas.FirstOrDefaultAsync(x => x.Id == id);

            if (planta is null)
            {
                return NotFound();
            }

            planta.Nome = atualizarPlanta.Nome;
            planta.NecessidadeHidricaId = atualizarPlanta.NecessidadeHidricaId;
            planta.SensorId = atualizarPlanta.SensorId;
            planta.RotinaId = atualizarPlanta.RotinaId;

            _context.Plantas.Update(planta);
            await _context.SaveChangesAsync();

            return Ok(planta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var planta = await _context.Plantas.FirstOrDefaultAsync(x => x.Id == id);

            if (planta is null)
            {
                return NotFound();
            }

            _context.Plantas.Remove(planta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
