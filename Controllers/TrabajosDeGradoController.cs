using GestionTrabajosDeGradoAPI.Interfaces;
using GestionTrabajosDeGradoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionTrabajosDeGradoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrabajosDeGradoController : ControllerBase
    {
        private readonly ITrabajosDeGradoRepository _trabajosDeGradoRepository;

        public TrabajosDeGradoController(ITrabajosDeGradoRepository trabajosDeGradoRepository)
        {
            _trabajosDeGradoRepository = trabajosDeGradoRepository;
        }


        // endpoint para obtener los trabajos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var trabajos = await _trabajosDeGradoRepository.GetAll(); 
            return Ok(trabajos);
        }

        // endpoint para obtener trabajos de grado por estudiante/s
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrabajosByEstudianteId(int id) 
        {
            var trabajos = await _trabajosDeGradoRepository.getByIdEstudiante(id);
            if (trabajos == null || !trabajos.Any()) 
            {
                return NotFound("No se tienen trabajos de grado asignados.");
            }
            return Ok(trabajos);    
        }

    }
}
