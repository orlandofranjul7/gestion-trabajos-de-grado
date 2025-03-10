using GestionTrabajosDeGradoAPI.Interfaces;
using GestionTrabajosDeGradoAPI.ViewModels; 
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAutenticacionRepository _autenticacionRepository;
    private readonly JwtService _jwtService;

    public AuthController(IAutenticacionRepository autenticacionRepository, JwtService jwtService)
    {
        _autenticacionRepository = autenticacionRepository;
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {

        Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:5173");
        Response.Headers.Add("Access-Control-Allow-Methods", "POST, OPTIONS");
        Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");

        try
        {
            var response = await _autenticacionRepository.LoginAsync(request);
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpPost("logout")]
    public IActionResult Logout() 
    {
        // El token se debe de eliminar en frontend
        return Ok(new { Message = "Sesión cerrada..." });
    }


}
