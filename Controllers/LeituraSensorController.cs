using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_IrrigaMais_API.DataContext;
using Projeto_IrrigaMais_API.Models;
using Projeto_IrrigaMais_API.Models.Dtos;

namespace Projeto_IrrigaMais_API.Controllers
{
    [Route("/leituraSensor")]
    [ApiController]
    public class LeituraSensorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LeituraSensorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos(
            [FromQuery] int? id
            )
        {
            var query = _context
                .LeiturasSensores
                .Include(s => s.Sensor)
                .AsQueryable();

            if (id is not null)
            {
                query = query.Where(x => x.Id == id);

                return Ok(query);
            }

            var leituraSensor = await query
                .Include(s => s.Sensor)
                .Select(l => new
                {
                    l.Id,
                    l.DtLeitura,
                    l.Valor,
                    Sensor = new { l.Sensor.Nome }

                }).ToListAsync();

            return Ok(leituraSensor);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var leituraSensor = await _context
                .LeiturasSensores
                .Include(s => s.Sensor)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (leituraSensor is null)
            {
                return NotFound();
            }

            return Ok(leituraSensor);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] LeituraSensorDto novaLeitura)
        {
            var sensor = await _context
                .Sensores
                .FirstOrDefaultAsync(x => x.Id == novaLeitura.SensorId);

            if (sensor is null)
            {
                return NotFound("Sensor não encontrado");
            }

            var leituraSensor = new LeituraSensor()
            {
                DtLeitura = DateTime.Now,
                Valor = novaLeitura.Valor,
                SensorId = novaLeitura.SensorId
            };

            await _context.LeiturasSensores.AddAsync(leituraSensor);
            await _context.SaveChangesAsync();

            return Created("", leituraSensor);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] LeituraSensorDto atualizarLeitura)
        {
            var leituraSensor = await _context.LeiturasSensores.FirstOrDefaultAsync(x => x.Id == id);

            if (leituraSensor is null)
            {
                return NotFound();
            }

            leituraSensor.DtLeitura = DateTime.Now;
            leituraSensor.Valor = atualizarLeitura.Valor;
            leituraSensor.SensorId = atualizarLeitura.SensorId;

            _context.LeiturasSensores.Update(leituraSensor);
            await _context.SaveChangesAsync();

            return Ok(leituraSensor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var leituraSensor = await _context.LeiturasSensores.FirstOrDefaultAsync(x => x.Id == id);


            if (leituraSensor is null)
            {
                return NotFound();
            }

            _context.LeiturasSensores.Remove(leituraSensor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
