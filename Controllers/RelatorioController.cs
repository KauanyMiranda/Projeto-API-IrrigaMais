using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> BuscarTodos(
            [FromQuery] string? buscar
            )
        {
            var query = _context.Relatorios.AsQueryable();
            if (buscar is not null)
            {
                query = query.Where(x => x.tipoRelatorio.Contains(buscar));

                return Ok(query);
            }

            var relatorio = await query
                .Include(u => u.Usuario)
                .Select(r => new 
                { 
                    r.tipoRelatorio,
                    r.dtGeracao,
                    r.DataInicial,
                    r.DataFinal,
                    Usuario = new { r.Usuario.Nome }
                }).ToListAsync();

            return Ok(relatorio);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var relatorio = await _context
                .Relatorios
                .Include(u => u.Usuario)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (relatorio is null)
            {
                return NotFound();
            }

            return Ok(relatorio);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] RelatorioDto novoRelatorio)
        {

            var usuario = await _context
                .Usuarios
                .FirstOrDefaultAsync(x => x.Id == novoRelatorio.UsuarioId);

            if (usuario is null)
            {
                return NotFound("Usuário não encontrado");
            }

            var relatorio = new Relatorio()
            {
                tipoRelatorio = novoRelatorio.TipoRelatorio,
                dtGeracao = DateTime.Now,
                DataInicial = novoRelatorio.DataInicial,
                DataFinal = novoRelatorio.DataFinal,
                UsuarioId = novoRelatorio.UsuarioId
            };


            await _context.Relatorios.AddAsync(relatorio);
            await _context.SaveChangesAsync();

            return Created("", relatorio);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] RelatorioDto atualizarRelatorio)
        {
            var relatorio = await _context.Relatorios.FirstOrDefaultAsync(x => x.Id == id);

            if (relatorio is null)
            {
                return NotFound();
            }

            relatorio.tipoRelatorio = atualizarRelatorio.TipoRelatorio;
            relatorio.DataInicial = atualizarRelatorio.DataInicial;
            relatorio.DataFinal = atualizarRelatorio.DataFinal;

            _context.Relatorios.Update(relatorio);
            await _context.SaveChangesAsync();

            return Ok(relatorio);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var relatorio = await _context.Relatorios.FirstOrDefaultAsync(x => x.Id == id);


            if (relatorio is null)
            {
                return NotFound();
            }

            _context.Relatorios.Remove(relatorio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
