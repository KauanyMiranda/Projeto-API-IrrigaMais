using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> BuscarTodos()
        {
            var query = _context.Irrigacoes.AsQueryable();


            var irrigacoes = await query
                .Include(l => l.LeituraSensor)
                .Include(a => a.Api)
                .Include(ri => ri.RotinasIrrigacoes)
                .ThenInclude(r => r.Rotina)
                .Select(i => new
                {
                    i.Id,
                    i.ConsumoHidrico,
                    i.DtInicial,
                    i.DtFinal,
                    LeituraSensor = new { i.LeituraSensor.Valor, i.LeituraSensor.DtLeitura },
                    Api = new { i.Api.Descricao, i.Api.Previsao, i.Api.DtConsulta },
                    Rotinas = i.RotinasIrrigacoes.Select(ri => new
                    {
                        ri.Rotina.Id,
                        ri.Rotina.nome_rotina,
                        ri.Rotina.tipo_execucao,
                        ri.Rotina.horario,
                        ri.Rotina.frequencia,
                        ri.Rotina.dia_seg,
                        ri.Rotina.dia_ter,
                        ri.Rotina.dia_qua,
                        ri.Rotina.dia_qui,
                        ri.Rotina.dia_sex,
                        ri.Rotina.dia_sab,
                        ri.Rotina.dia_dom
                    }).ToList()
                }).ToListAsync();

            return Ok(irrigacoes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var irrigacao = await _context.Irrigacoes
                .Include(l => l.LeituraSensor)
                .Include(a => a.Api)
                .Include(ri => ri.RotinasIrrigacoes)
                .ThenInclude(r => r.Rotina)
                .FirstOrDefaultAsync(x => x.Id == id);

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
                .FirstOrDefaultAsync(x => x.Id == novaIrrigacao.LeituraSensorId);

            if (leituraSensor is null)
            {
                return NotFound("Leitura de sensor não encontrada");
            }

            var api = await _context.Apis
                .FirstOrDefaultAsync(x => x.Id == novaIrrigacao.ApiId);

            if (api is null)
            {
                return NotFound("API informada não encontrada");
            }

            var irrigacao = new Irrigacao()
            {
                ConsumoHidrico = novaIrrigacao.ConsumoHidrico,
                DtInicial = novaIrrigacao.DataInicial,
                DtFinal = novaIrrigacao.DataFinal,
                LeituraSensorId = novaIrrigacao.LeituraSensorId,
                ApiId = novaIrrigacao.ApiId
            };

            await _context.Irrigacoes.AddAsync(irrigacao);
            await _context.SaveChangesAsync();

            return Created("", irrigacao);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] IrrigacaoDto atualizarIrrigacao)
        {
            var irrigacao = await _context.Irrigacoes
                .Include(ri => ri.RotinasIrrigacoes)
                .ThenInclude(r => r.Rotina)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (irrigacao is null)
            {
                return NotFound();
            }

            irrigacao.ConsumoHidrico = atualizarIrrigacao.ConsumoHidrico;
            irrigacao.DtInicial = atualizarIrrigacao.DataInicial;
            irrigacao.DtFinal = atualizarIrrigacao.DataFinal;
            irrigacao.LeituraSensorId = atualizarIrrigacao.LeituraSensorId;
            irrigacao.ApiId = atualizarIrrigacao.ApiId;

            _context.Irrigacoes.Update(irrigacao);
            await _context.SaveChangesAsync();

            return Ok(irrigacao);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var irrigacao = await _context.Irrigacoes
                .Include(ri => ri.RotinasIrrigacoes)
                .ThenInclude(r => r.Rotina)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (irrigacao is null)
            {
                return NotFound();
            }

            _context.Irrigacoes.Remove(irrigacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id}/adicionar-rotina")]
        public async Task<IActionResult> AdicionarRotina(int id, [FromBody] RotinaIrrigacaoDto rotinas)
        {
            var irrigacao = await _context.Irrigacoes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (irrigacao is null)
            {
                return NotFound();
            }

            var _rotinas = await _context.Rotinas
                .Where(r => rotinas.RotinasIds.Contains(r.Id))
                .ToListAsync();

            if (_rotinas.Count != rotinas.RotinasIds.Count)
            {
                return NotFound("Uma ou mais rotinas não foram encontradas");
            }

            irrigacao.RotinasIrrigacoes = _rotinas.Select(ri => new RotinaIrrigacao
            {
                RotinaId = ri.Id,
                IrrigacaoId = irrigacao.Id

            }).ToList();

            _context.Irrigacoes.Update(irrigacao);
            await _context.SaveChangesAsync();

            return Ok(irrigacao);
        }

        [HttpPost("{id}/remover-rotina")]
        public async Task<IActionResult> RemoverUsuarioPorId(int id, int rotinaId)
        {
            var irrigacao = await _context.Irrigacoes.Include(c => c.RotinasIrrigacoes).FirstOrDefaultAsync(x => x.Id == id);

            if (irrigacao is null)
            {
                return NotFound();
            }

            var rotinaIrrigacao = irrigacao.RotinasIrrigacoes.FirstOrDefault(c => c.RotinaId == rotinaId);

            if (rotinaIrrigacao is null)
            {
                return NotFound("Rotina não está vinculada a irrigação");
            }

            irrigacao.RotinasIrrigacoes.Remove(rotinaIrrigacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}