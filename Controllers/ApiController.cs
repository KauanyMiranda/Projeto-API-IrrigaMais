using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_IrrigaMais_API.Models;
using Projeto_IrrigaMais_API.Models.Dtos;

namespace Projeto_IrrigaMais_API.Controllers
{
    [Route("/api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly DataContext.AppDbContext _context;
        public ApiController(DataContext.AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos([FromQuery] string? cidade)
        {
            var query = _context.Apis.AsQueryable();

            if (cidade is not null)
            {
                query = query.Where(a => a.Cidade.Contains(cidade));
            }

            var apis = await query
                .Select(a => new
                {
                    a.Id,
                    a.Cidade,
                    a.Pais,
                    a.Descricao,
                    a.Icone,
                    a.Temp,
                    a.TempMax,
                    a.TempMin,
                    a.Previsao,
                    a.Umidade,
                    a.Vento,
                    a.DtConsulta
                })
                .ToListAsync();

            return Ok(apis);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var api = await _context.Apis
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
                Cidade = novaApi.Cidade,
                Pais = novaApi.Pais,
                Descricao = novaApi.Descricao,
                Icone = novaApi.Icone,
                Temp = novaApi.Temp,
                TempMax = novaApi.TempMax,
                TempMin = novaApi.TempMin,
                Previsao = novaApi.Previsao,
                Umidade = novaApi.Umidade,
                Vento = novaApi.Vento,
            };

            await _context.Apis.AddAsync(api);
            await _context.SaveChangesAsync();

            return Created("", api);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] ApiDto atualizarApi)
        {
            var api = await _context.Apis.FirstOrDefaultAsync(a => a.Id == id);

            if (api is null)
            {
                return NotFound();
            }

            api.Cidade = atualizarApi.Cidade;
            api.Pais = atualizarApi.Pais;
            api.Descricao = atualizarApi.Descricao;
            api.Icone = atualizarApi.Icone;
            api.Temp = atualizarApi.Temp;
            api.TempMax = atualizarApi.TempMax;
            api.TempMin = atualizarApi.TempMin;
            api.Previsao = atualizarApi.Previsao;
            api.Umidade = atualizarApi.Umidade;
            api.Vento = atualizarApi.Vento;

            _context.Apis.Update(api);
            await _context.SaveChangesAsync();

            return Ok(api);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var api = await _context.Apis.FirstOrDefaultAsync(a => a.Id == id);

            if (api is null)
            {
                return NotFound();
            }

            _context.Apis.Remove(api);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
