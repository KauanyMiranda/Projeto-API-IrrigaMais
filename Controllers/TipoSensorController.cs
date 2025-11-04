using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_IrrigaMais_API.DataContext;
using Projeto_IrrigaMais_API.Models;
using Projeto_IrrigaMais_API.Models.Dtos;

namespace Projeto_IrrigaMais_API.Controllers
{
    [Route("/sensor")]
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
            var tipoSensor = await _context.tipoSensores.ToListAsync();

            return Ok(tipoSensor);
        }
    }
}
