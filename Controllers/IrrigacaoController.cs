using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_IrrigaMais_API.DataContext;
using Projeto_IrrigaMais_API.Models;
using Projeto_IrrigaMais_API.Models.Dtos;

namespace Projeto_IrrigaMais_API.Controllers
{
    [Route("/irrigacao")]
    [ApiController]
    public class IrrigacaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IrrigacaoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos(
            [FromQuery] string? modo,
            [FromQuery] int? sensorId
        )
        {
            var query = _context.Irrigacao.AsQueryable();

            if (modo is not null)
            {
                query = query.Where(x => x.modo_irrigacao.Contains(modo));
            }

            if (sensorId is not null)
            {
                query = query.Where(x => x.fk_id_leitura_sensor == sensorId);
            }

            var irrigacoes = await query
                .Select(i => new
                {
                    i.id,
                    i.modo_irrigacao,
                    i.consumo_hidrico,
                    i.dt_inicial,
                    i.dt_final,
                    Sensor = new { id = i.fk_id_leitura_sensor },
                    Api = new { id = i.fk_id_api }
                })
                .ToListAsync();

            return Ok(irrigacoes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var irrigacao = await _context.Irrigacao
                .FirstOrDefaultAsync(x => x.id == id);

            if (irrigacao is null)
            {
                return NotFound();
            }

            return Ok(irrigacao);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] IrrigacaoDto novaIrrigacao)
        {
            var leituraSensor = await _context.LeiturasSensores
                .FirstOrDefaultAsync(x => x.Id == novaIrrigacao.fk_id_leitura_sensor);

            if (leituraSensor is null)
            {
                return NotFound("Leitura de sensor não encontrada");
            }

            var api = await _context.Api
                .FirstOrDefaultAsync(x => x.Id == novaIrrigacao.fk_id_api);

            if (api is null)
            {
                return NotFound("API informada não encontrada");
            }

            var irrigacao = new Irrigacao()
            {
                modo_irrigacao = novaIrrigacao.modo_irrigacao,
                consumo_hidrico = novaIrrigacao.consumo_hidrico,
                dt_inicial = novaIrrigacao.DataInicial,
                dt_final = novaIrrigacao.DataFinal,
                fk_id_leitura_sensor = novaIrrigacao.fk_id_leitura_sensor,
                fk_id_api = novaIrrigacao.fk_id_api
            };

            await _context.Irrigacao.AddAsync(irrigacao);
            await _context.SaveChangesAsync();

            return Created("", irrigacao);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] IrrigacaoDto atualizarIrrigacao)
        {
            var irrigacao = await _context.Irrigacao.FirstOrDefaultAsync(x => x.id == id);

            if (irrigacao is null)
            {
                return NotFound();
            }

            irrigacao.modo_irrigacao = atualizarIrrigacao.modo_irrigacao;
            irrigacao.consumo_hidrico = atualizarIrrigacao.consumo_hidrico;
            irrigacao.dt_inicial = atualizarIrrigacao.DataInicial;
            irrigacao.dt_final = atualizarIrrigacao.DataFinal;
            irrigacao.fk_id_leitura_sensor = atualizarIrrigacao.fk_id_leitura_sensor;
            irrigacao.fk_id_api = atualizarIrrigacao.fk_id_api;

            _context.Irrigacao.Update(irrigacao);
            await _context.SaveChangesAsync();

            return Ok(irrigacao);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var irrigacao = await _context.Irrigacao.FirstOrDefaultAsync(x => x.id == id);

            if (irrigacao is null)
            {
                return NotFound();
            }

            _context.Irrigacao.Remove(irrigacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
