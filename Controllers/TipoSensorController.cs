using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_IrrigaMais_API.DataContext;

namespace Projeto_IrrigaMais_API.Controllers
{
    [Route("/tipoSensor")]
    [ApiController]
    public class TipoSensorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TipoSensorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var tipoSensor = await _context
                .TipoSensores
                .ToListAsync();

            return Ok(tipoSensor);
        }
    }
}