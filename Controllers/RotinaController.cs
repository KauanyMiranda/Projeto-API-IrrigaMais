using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_IrrigaMais_API.DataContext;
using Projeto_IrrigaMais_API.Models;
using Projeto_IrrigaMais_API.Models.Dtos;
using System.Numerics;

namespace Projeto_IrrigaMais_API.Controllers
{
    [Route("/rotinas")]
    [ApiController]
    public class RotinaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RotinaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos(
            [FromQuery] string? buscar
            )
        {
            var query = _context.Rotinas.AsQueryable();
            if (buscar is not null)
            {
                query = query.Where(x => x.NomeRotina.Contains(buscar));

                return Ok(query);
            }

            var rotinas = await query
                .Include(u => u.Usuario)
                .Select(r => new
                {   r.Id,
                    r.NomeRotina,
                    r.TipoExecucao,
                    r.Horario,
                    r.Frequencia,
                    r.DiaSeg,
                    r.DiaTer,
                    r.DiaQua,
                    r.DiaQui,
                    r.DiaSex,
                    r.DiaSab,
                    r.DiaDom,
                    Usuario = new { r.Usuario.Nome }
                }).ToListAsync();

            return Ok(rotinas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var rotina = await _context
                .Rotinas
                .FirstOrDefaultAsync(x => x.Id == id);

            if (rotina is null)
            {
                return NotFound();
            }

            return Ok(rotina);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] RotinaDto novaRotina)
        {

            var rotina = new Rotina()
            {
                NomeRotina = novaRotina.NomeRotina,
                TipoExecucao = novaRotina.TipoExecucao,
                Horario = novaRotina.Horario,
                Frequencia  = novaRotina.Frequencia,
                DiaSeg = novaRotina.DiaSeg,
                DiaTer = novaRotina.DiaTer,
                DiaQua = novaRotina.DiaQua,
                DiaQui = novaRotina.DiaQui,
                DiaSex = novaRotina.DiaSex,
                DiaSab = novaRotina.DiaSab,
                DiaDom = novaRotina.DiaDom,
                UsuarioId = novaRotina.UsuarioId
            };

            await _context.Rotinas.AddAsync(rotina);
            await _context.SaveChangesAsync();

            return Created("", rotina);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] RotinaDto atualizarRotina)
        {
            var rotina = await _context
                .Rotinas
                .FirstOrDefaultAsync(x => x.Id == id);

            if (rotina is null)
            {
                return NotFound();
            }

            rotina.NomeRotina = atualizarRotina.NomeRotina;
            rotina.TipoExecucao = atualizarRotina.TipoExecucao;
            rotina.Horario = atualizarRotina.Horario;
            rotina.Frequencia = atualizarRotina.Frequencia;
            rotina.DiaSeg = atualizarRotina.DiaSeg;
            rotina.DiaTer = atualizarRotina.DiaTer;
            rotina.DiaQua = atualizarRotina.DiaQua;
            rotina.DiaQui = atualizarRotina.DiaQui;
            rotina.DiaSex = atualizarRotina.DiaSex;
            rotina.DiaSab = atualizarRotina.DiaSab;
            rotina.DiaDom = atualizarRotina.DiaDom;
            rotina.UsuarioId = atualizarRotina.UsuarioId;

            _context.Rotinas.Update(rotina);
            await _context.SaveChangesAsync();

            return Ok(rotina);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var rotina = await _context.Rotinas.FirstOrDefaultAsync(x => x.Id == id);

            if (rotina is null)
            {
                return NotFound();
            }
                
            _context.Rotinas.Remove(rotina);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
