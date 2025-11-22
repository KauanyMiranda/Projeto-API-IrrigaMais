using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_IrrigaMais_API.DataContext;
using Projeto_IrrigaMais_API.Models;
using Projeto_IrrigaMais_API.Models.Dtos;

namespace Projeto_IrrigaMais_API.Controllers
{
    [Route("/api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ApiController(AppDbContext context)
        {
            _context = context;
        }

       
        [HttpGet]
        public async Task<IActionResult> BuscarTodos([FromQuery] string? cidade)
        {
            var query = _context.Api.AsQueryable();

            if (cidade is not null)
            {
                query = query.Where(a => a.cidade.Contains(cidade));
            }

            var apis = await query
                .Include(a => a.Irrigacao)
                .Select(a => new
                {
                    a.Id,
                    a.cidade,
                    a.pais,
                    a.descricao,
                    a.icone,
                    a.temp_max,
                    a.temp_min,
                    a.previsao,
                    a.umidade,
                    a.vento,
                    a.dt_consulta,
                    Irrigacoes = a.Irrigacao.Select(i => new
                    {
                        i.id,
                        i.modo_irrigacao,
                        i.consumo_hidrico,
                        i.dt_inicial,
                        i.dt_final
                    })
                })
                .ToListAsync();

            return Ok(apis);
        }

      
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var api = await _context.Api
                .Include(a => a.Irrigacao)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (api is null)
            {
                return NotFound();
            }

            return Ok(api);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] ApiDto novaApi)
        {
            var api = new Api()
            {
                cidade = novaApi.cidade,
                pais = novaApi.pais,
                descricao = novaApi.descricao,
                icone = novaApi.icone,
                temp_max = novaApi.temp_max,
                temp_min = novaApi.temp_min,
                previsao = novaApi.previsao,
                umidade = novaApi.umidade,
                vento = novaApi.vento,
            };

            await _context.Api.AddAsync(api);
            await _context.SaveChangesAsync();

            return Created("", api);
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] ApiDto atualizarApi)
        {
            var api = await _context.Api.FirstOrDefaultAsync(a => a.Id == id);

            if (api is null)
            {
                return NotFound();
            }

            api.cidade = atualizarApi.cidade;
            api.pais = atualizarApi.pais;
            api.descricao = atualizarApi.descricao;
            api.icone = atualizarApi.icone;
            api.temp_max = atualizarApi.temp_max;
            api.temp_min = atualizarApi.temp_min;
            api.previsao = atualizarApi.previsao;
            api.umidade = atualizarApi.umidade;
            api.vento = atualizarApi.vento;

            _context.Api.Update(api);
            await _context.SaveChangesAsync();

            return Ok(api);
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var api = await _context.Api.FirstOrDefaultAsync(a => a.Id == id);

            if (api is null)
            {
                return NotFound();
            }

            _context.Api.Remove(api);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
