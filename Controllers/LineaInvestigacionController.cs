using GestionTrabajosDeGradoAPI.Helpers;
using GestionTrabajosDeGradoAPI.Interfaces;
using GestionTrabajosDeGradoAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionTrabajosDeGradoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LineaInvestigacionController : Controller
    {

        private readonly ILineaInvestigacionRepository _lineaInvestigacionRepository;

        public LineaInvestigacionController(ILineaInvestigacionRepository lineaInvestigacionRepository)
        {
            _lineaInvestigacionRepository = lineaInvestigacionRepository;   
        }

        [HttpGet("investigaciones")]
        public async Task<IActionResult> GetInvestigacionesPorUsuario()
        {
            try
            {
                int idUsuario = TokenHelper.GetUserIdToken(User);
                var investigaciones = await _lineaInvestigacionRepository.getInvestigacionPerEscuela(idUsuario);

                if (investigaciones == null || investigaciones.Count == 0)
                    return NotFound(new { mensaje = "No hay investigaciones disponibles." });

                return Ok(investigaciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
