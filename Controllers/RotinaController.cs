using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_IrrigaMais_API.DataContext;
using Projeto_IrrigaMais_API.Models.Dtos;
using ApiServico.Models;

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
        public async Task<IActionResult> BuscarTodos()
        {
            var rotinas = await _context.Rotinas.ToListAsync();
            return Ok(rotinas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var rotina = await _context.Rotinas.FirstOrDefaultAsync(x => x.Id == id);

            if (rotina == null)
                return NotFound();

            return Ok(rotina);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] RotinaDto novaRotina)
        {
            var rotina = new Rotina()
            {
                tipo_execucao = novaRotina.tipo_execucao,
                horario = novaRotina.horario,
                frequencia = novaRotina.frequencia,
                nome_rotina = novaRotina.nome_rotina,

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
        public async Task<IActionResult> Atualizar(int id, [FromBody] RotinaDto atualizar)
        {
            var rotina = await _context.Rotinas.FirstOrDefaultAsync(x => x.Id == id);

            if (rotina == null)
                return NotFound();

            rotina.tipo_execucao = atualizar.tipo_execucao;
            rotina.horario = atualizar.horario;
            rotina.frequencia = atualizar.frequencia;
            rotina.nome_rotina = atualizar.nome_rotina;

            rotina.dia_seg = atualizar.dia_seg;
            rotina.dia_ter = atualizar.dia_ter;
            rotina.dia_qua = atualizar.dia_qua;
            rotina.dia_qui = atualizar.dia_qui;
            rotina.dia_sex = atualizar.dia_sex;
            rotina.dia_sab = atualizar.dia_sab;
            rotina.dia_dom = atualizar.dia_dom;

            _context.Rotinas.Update(rotina);
            await _context.SaveChangesAsync();

            return Ok(rotina);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var rotina = await _context.Rotinas.FirstOrDefaultAsync(x => x.Id == id);

            if (rotina == null)
                return NotFound();

            _context.Rotinas.Remove(rotina);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
