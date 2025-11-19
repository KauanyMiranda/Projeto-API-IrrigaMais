using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_IrrigaMais_API.DataContext;
using Projeto_IrrigaMais_API.Models;
using Projeto_IrrigaMais_API.Models.Dtos;

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
        public async Task<IActionResult> BuscarTodos(
            [FromQuery] string? buscar
            )
        {
            var query = _context.Usuarios.AsQueryable();

            if (buscar is not null)
            {
                query = query.Where(x => x.Nome.Contains(buscar));

                return Ok(query);
            }

            var usuarios = await query.Select(u => new
            {
                u.Id,
                u.Nome,
                u.Email
            }).ToListAsync();

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(x => x.Id == id);

            if (usuario is null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] UsuarioDto novoUsuario)
        {
            var usuario = new Usuario()
            {
                Nome = novoUsuario.Nome,
                Email = novoUsuario.Email,
                Senha = novoUsuario.Senha
            };

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return Created("", usuario);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] UsuarioDto atualizarUsuario)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

            if (usuario is null)
            {
                return NotFound();
            }

            usuario.Nome = atualizarUsuario.Nome;
            usuario.Email = atualizarUsuario.Email;
            usuario.Senha = atualizarUsuario.Senha;

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            
            if (usuario is null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
