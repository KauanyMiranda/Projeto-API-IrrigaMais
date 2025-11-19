using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_IrrigaMais_API.DataContext;
using Projeto_IrrigaMais_API.Models;
using Projeto_IrrigaMais_API.Models.Dtos;

namespace Projeto_IrrigaMais_API.Controllers
{
    [Route("/necessidadeHidrica")]
    [ApiController]
    public class NecessidadeHidricaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NecessidadeHidricaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var necessidadeHidrica = await _context
                .necessidadesHidricas.ToListAsync();

            return Ok(necessidadeHidrica);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var necessidadeHidrica = await _context
                .necessidadesHidricas
                .FirstOrDefaultAsync(x => x.Id == id);

            if (necessidadeHidrica is null)
            {
                return NotFound();
            }

            return Ok(necessidadeHidrica);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] NecessidadeHidricaDto novaNecessidade)
        {
            var necessidadeHidrica = new NecessidadeHidrica()
            {
                Nome = novaNecessidade.Nome,
                qtdLitros = novaNecessidade.qtdlitros
            };

            await _context.necessidadesHidricas.AddAsync(necessidadeHidrica);
            await _context.SaveChangesAsync();

            return Created("", necessidadeHidrica);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] NecessidadeHidricaDto atualizarNecessidade)
        {
            var necessidadeHidrica = await _context.necessidadesHidricas.FirstOrDefaultAsync(x => x.Id == id);

            if (necessidadeHidrica is null)
            {
                return NotFound();
            }

            necessidadeHidrica.Nome = atualizarNecessidade.Nome;
            necessidadeHidrica.qtdLitros = atualizarNecessidade.qtdlitros;

            _context.necessidadesHidricas.Update(necessidadeHidrica);
            await _context.SaveChangesAsync();

            return Ok(necessidadeHidrica);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var necessidadeHidrica = await _context.necessidadesHidricas.FirstOrDefaultAsync(x => x.Id == id);


            if (necessidadeHidrica is null)
            {
                return NotFound();
            }

            _context.necessidadesHidricas.Remove(necessidadeHidrica);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
