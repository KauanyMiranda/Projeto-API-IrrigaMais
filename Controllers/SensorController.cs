using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_IrrigaMais_API.DataContext;
using Projeto_IrrigaMais_API.Models;
using Projeto_IrrigaMais_API.Models.Dtos;
using System.Numerics;

namespace Projeto_IrrigaMais_API.Controllers
{
    [Route("/sensor")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SensorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos(
            [FromQuery] string? buscar
            )
        {
            var query = _context.Sensores.AsQueryable();
            if (buscar is not null)
            {
                query = query.Where(x => x.Nome.Contains(buscar));

                return Ok(query);
            }

            var sensores = await query
                .Include(t => t.TipoSensor)
                .Include(u => u.Usuario)
                .Select(s => new
                {
                    s.Nome,
                    s.Localizacao,
                    TipoSensor = new { s.TipoSensor.Nome },
                    Usuario = new {s.Usuario.Nome},
                    s.StatusSensor
                }).ToListAsync();

            return Ok(sensores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var sensor = await _context.Sensores
                .Include(t => t.TipoSensor)
                .Include(u => u.Usuario)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (sensor is null)
            {
                return NotFound();
            }

            return Ok(sensor);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] SensorDto novoSensor)
        {

            var tipoSensor = await _context
                .TipoSensores
                .FirstOrDefaultAsync(x => x.Id == novoSensor.TipoSensorId);

            var usuario = await _context
                .Usuarios
                .FirstOrDefaultAsync(x => x.Id == novoSensor.UsuarioId);

            if (tipoSensor is null)
            {
                return NotFound("Tipo de Sensor não encontrado");
            }

            if (usuario is null)
            {
                return NotFound("Usuário não encontrado");
            }

            var sensor = new Sensor()
            {
                Nome = novoSensor.Nome,
                Localizacao = novoSensor.Localizacao,
                TipoSensorId = novoSensor.TipoSensorId,
                UsuarioId = novoSensor.UsuarioId
            };

            await _context.Sensores.AddAsync(sensor);
            await _context.SaveChangesAsync();

            return Created("", sensor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] SensorDto atualizarSensor)
        {
            var sensor = await _context.Sensores.FirstOrDefaultAsync(x => x.Id == id);

            if (sensor is null)
            {
                return NotFound();
            }

            sensor.Nome = atualizarSensor.Nome;
            sensor.Localizacao = atualizarSensor.Localizacao;
            sensor.TipoSensorId = atualizarSensor.TipoSensorId;

            _context.Sensores.Update(sensor);
            await _context.SaveChangesAsync();

            return Ok(sensor);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var sensor = await _context.Sensores.FirstOrDefaultAsync(x => x.Id == id);

            if (sensor is null)
            {
                return NotFound();
            }

            _context.Sensores.Remove(sensor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
