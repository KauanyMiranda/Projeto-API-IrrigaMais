using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_IrrigaMais_API.DataContext;
using Projeto_IrrigaMais_API.Models;
using Projeto_IrrigaMais_API.Models.Dtos;

namespace Projeto_IrrigaMais_API.Controllers
{
    [Route("/tipoSensor")]
    [ApiController]
    public class TipoSensorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TipoSensorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var tipoSensor = await _context
                .TipoSensores
                .ToListAsync();

            return Ok(tipoSensor);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var tipoSensores = await _context
                .TipoSensores
                .FirstOrDefaultAsync(x => x.Id == id);

            if (tipoSensores is null)
            {
                return NotFound();
            }

            return Ok(tipoSensores);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] TipoSensorDto novoTipoSensor)
        {
            var tipoSensor = new TipoSensor()
            {
                Nome = novoTipoSensor.Nome,
                UnidadeMedida = novoTipoSensor.UnidadeMedida
            };

            await _context.TipoSensores.AddAsync(tipoSensor);
            await _context.SaveChangesAsync();

            return Created("", tipoSensor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] TipoSensorDto atualizarTipoSensor)
        {
            var tipoSensor = await _context.TipoSensores.FirstOrDefaultAsync(x => x.Id == id);

            if (tipoSensor is null)
            {
                return NotFound();
            }

            tipoSensor.Nome = atualizarTipoSensor.Nome;
            tipoSensor.UnidadeMedida = atualizarTipoSensor.UnidadeMedida;

            _context.TipoSensores.Update(tipoSensor);
            await _context.SaveChangesAsync();

            return Ok(tipoSensor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var tipoSensor = await _context.TipoSensores.FirstOrDefaultAsync(x => x.Id == id);


            if (tipoSensor is null)
            {
                return NotFound();
            }

            _context.TipoSensores.Remove(tipoSensor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}