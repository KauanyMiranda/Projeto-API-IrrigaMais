using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_IrrigaMais_API.DataContext;

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
    }
}
