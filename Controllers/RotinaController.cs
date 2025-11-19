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
            if ( buscar is not null)
            {
                query = query.Where(x => x.nome_rotina.Contains(buscar));

                return Ok(query);
            }

            var rotinas = await query.Select(r => new 
            { r.Id,
              r.nome_rotina,
              r.tipo_execucao,
              r.horario,
              r.frequencia,
              r.dia_seg,
              r.dia_ter,
              r.dia_qua,
              r.dia_qui,
              r.dia_sex,
              r.dia_sab,
              r.dia_dom
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
                nome_rotina = novaRotina.nome_rotina,
                tipo_execucao = novaRotina.tipo_execucao,
                horario = novaRotina.horario,
                frequencia = novaRotina.frequencia,
                dia_seg = novaRotina.dia_seg,
                dia_ter = novaRotina.dia_ter,
                dia_qua = novaRotina.dia_qua,
                dia_qui = novaRotina.dia_qui,
                dia_sex = novaRotina.dia_sex,
                dia_sab = novaRotina.dia_sab,
                dia_dom = novaRotina.dia_dom
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

            rotina.nome_rotina = atualizarRotina.nome_rotina;
            rotina.tipo_execucao = atualizarRotina.tipo_execucao;
            rotina.horario = atualizarRotina.horario;
            rotina.frequencia = atualizarRotina.frequencia;
            rotina.dia_seg = atualizarRotina.dia_seg;
            rotina.dia_ter = atualizarRotina.dia_ter;
            rotina.dia_qua = atualizarRotina.dia_qua;
            rotina.dia_qui = atualizarRotina.dia_qui;
            rotina.dia_sex = atualizarRotina.dia_sex;
            rotina.dia_sab = atualizarRotina.dia_sab;
            rotina.dia_dom = atualizarRotina.dia_dom;


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
