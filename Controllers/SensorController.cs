using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_IrrigaMais_API.DataContext;
using Projeto_IrrigaMais_API.Models;
using Projeto_IrrigaMais_API.Models.Dtos;

namespace Projeto_IrrigaMais_API.Controllers
{
    [Route("/")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public SensorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] SensorDto novoSensor)
        {
            var tipoSensor = await _context
                .tipoSensores
                .FirstOrDefaultAsync(x => x.Id == novoSensor.TipoSensorId);

            if (tipoSensor is null)
            {
                return NotFound("Tipo de sensor não encontrado");
            }

            var sensor = new Sensor()
            {
                Nome = novoSensor.Nome,
                Localizacao = novoSensor.Localizacao,
                TipoSensorId = novoSensor.TipoSensorId

            };


            await _context.Sensores.AddAsync(sensor);
            await _context.SaveChangesAsync();

            return Created("", sensor);
        }
    }
}
