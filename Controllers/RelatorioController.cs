using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_IrrigaMais_API.DataContext;
using Projeto_IrrigaMais_API.Models;
using Projeto_IrrigaMais_API.Models.Dtos;

namespace Projeto_IrrigaMais_API.Controllers
{
    [Route("/relatorios")]
    [ApiController]
    public class RelatorioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RelatorioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var relatorios = await _context.Relatorios.ToListAsync();
            return Ok(relatorios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var relatorio = await _context.Relatorios
                .FirstOrDefaultAsync(x => x.Id == id);

            if (relatorio is null)
                return NotFound();

            return Ok(relatorio);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] RelatorioDto novoRelatorio)
        {
            if (novoRelatorio.DataInicial is null || novoRelatorio.DataFinal is null)
                return BadRequest("Data Inicial e Data Final são obrigatórias");

            var relatorio = new Relatorio()
            {
                tipoRelatorio = novoRelatorio.TipoRelatorio,
                dtGeracao = DateTime.Now,
                DataInicial = novoRelatorio.DataInicial,
                DataFinal = novoRelatorio.DataFinal
            };

            await _context.Relatorios.AddAsync(relatorio);
            await _context.SaveChangesAsync();

            return Created("", relatorio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] RelatorioDto atualizar)
        {
            var relatorio = await _context.Relatorios.FirstOrDefaultAsync(x => x.Id == id);

            if (relatorio is null)
                return NotFound();

            relatorio.tipoRelatorio = atualizar.TipoRelatorio ?? relatorio.tipoRelatorio;
            relatorio.DataInicial = atualizar.DataInicial ?? relatorio.DataInicial;
            relatorio.DataFinal = atualizar.DataFinal ?? relatorio.DataFinal;

            _context.Relatorios.Update(relatorio);
            await _context.SaveChangesAsync();

            return Ok(relatorio);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var relatorio = await _context.Relatorios.FirstOrDefaultAsync(x => x.Id == id);

            if (relatorio is null)
                return NotFound();

            _context.Relatorios.Remove(relatorio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
