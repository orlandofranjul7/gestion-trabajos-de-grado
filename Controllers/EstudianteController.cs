using GestionTrabajosDeGradoAPI.Data;
using GestionTrabajosDeGradoAPI.Helpers;
using GestionTrabajosDeGradoAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GestionTrabajosDeGradoAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IEstudianteRepository _estudianteRepository;

        public EstudianteController(IEstudianteRepository estudianteRepository, AppDbContext context)
        {
            _context = context;
            _estudianteRepository = estudianteRepository;
        }


        [HttpGet("por-escuela")]
        public async Task<IActionResult> ObtenerEstudiantesPorEscuela()
        {
            try
            {
                // Obtener ID del usuario desde el token
                int idUsuario = TokenHelper.GetUserIdToken(User);

                var estudiantes = await _estudianteRepository.GetEstudiantesPorEscuelaAsync(idUsuario);

                if (estudiantes == null || estudiantes.Count == 0)
                    return NotFound("No se encontraron estudiantes para la escuela del usuario.");

                return Ok(estudiantes);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
