using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_IrrigaMais_API.DataContext;
using ApiServico.Models;
using System.Threading.Tasks;

namespace Projeto_IrrigaMais_API.Controllers
{
    [Route("/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos([FromQuery] string? buscar)
        {
            var query = _context.Usuarios.AsQueryable();

            if (buscar is not null)
            {
                query = query.Where(x => x.Nome.Contains(buscar) || x.Email.Contains(buscar));
                return Ok(await query.ToListAsync());
            }

            var usuarios = await query
                .Select(u => new
                {
                    u.Id,
                    u.Nome,
                    u.Email
                })
                .ToListAsync();

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(x => x.Id == id);

            if (usuario is null)
                return NotFound();

            return Ok(new
            {
                usuario.Id,
                usuario.Nome,
                usuario.Email
            });
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] Usuario novoUsuario)
        {
            if (string.IsNullOrWhiteSpace(novoUsuario.Senha))
                return BadRequest("A senha é obrigatória.");

            await _context.Usuarios.AddAsync(novoUsuario);
            await _context.SaveChangesAsync();

            return Created("", new
            {
                novoUsuario.Id,
                novoUsuario.Nome,
                novoUsuario.Email
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Usuario atualizar)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

            if (usuario is null)
                return NotFound();

            usuario.Nome = atualizar.Nome;
            usuario.Email = atualizar.Email;

            if (!string.IsNullOrWhiteSpace(atualizar.Senha))
                usuario.Senha = atualizar.Senha;

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                usuario.Id,
                usuario.Nome,
                usuario.Email
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

            if (usuario is null)
                return NotFound();

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
