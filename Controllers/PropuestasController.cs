using GestionTrabajosDeGradoAPI.Helpers;
using GestionTrabajosDeGradoAPI.Interfaces;
using GestionTrabajosDeGradoAPI.Models;
using GestionTrabajosDeGradoAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GestionTrabajosDeGradoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropuestasController : ControllerBase
    {
        private readonly IPropuestasService _propuestasService;

        public PropuestasController(IPropuestasService propuestasService)
        {
            _propuestasService = propuestasService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var propuestas = await _propuestasService.GetAllAsync();
            return Ok(propuestas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var propuesta = await _propuestasService.GetByIdDetailsAsync(id);

            if (propuesta != null)
            {
                return Ok(propuesta);
            }

            return NotFound("La propuesta no fue encontrada.");

        }

        [HttpGet("mis-propuestas")]
        public async Task<IActionResult> ObtenerPropuestasPorRol()
        {
            try
            {
                // Obtener ID del usuario desde el token
                int idUsuario = TokenHelper.GetUserIdToken(User);

                // Llamar al servicio para obtener propuestas segun el rol (estudiante o director)
                var propuestas = await _propuestasService.GetPropuestasPerUsers(idUsuario, User);

                if (propuestas == null || !propuestas.Any())
                {
                    return NotFound("No se encontraron propuestas para este usuario.");
                }

                return Ok(propuestas);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpPost("agregar")]
        public async Task<IActionResult> AgregarPropuesta([FromBody] PropuestaRequest request)
        {
            try
            {
                // Llamar al servicio para agregar la propuesta
                var response = await _propuestasService.AddAsync(request, User);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("modificar")]
        public async Task<IActionResult> ModificarPropuesta([FromBody] PropuestaRequest request)
        {
            try
            {
                // Validar que se reciba un ID en el cuerpo
                if (request.Id <= 0)
                {
                    return BadRequest(new { error = "El identificador de la propuesta es requerido." });
                }

                // Llamar al servicio para modificar la propuesta
                var response = await _propuestasService.ModificarPropuesta(request, User);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarPropuesta(int id)
        {
            try
            {
                // Llamar al servicio para eliminar la propuesta
                var eliminado = await _propuestasService.EliminarPropuestaAsync(id, User);

                if (eliminado)
                {
                    return Ok(new { mensaje = "Propuesta eliminada correctamente (estado: Eliminado)." });
                }

                return NotFound(new { mensaje = "Propuesta no encontrada." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }





        /*
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] propuesta propuesta) 
        {
            var propuestaNueva = await _propuestasService.AddAsync(propuesta);
            return CreatedAtAction(nameof(GetById), new { id = propuestaNueva.id }, propuestaNueva);
        }
        */

    }
}
