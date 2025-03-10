using GestionTrabajosDeGradoAPI.Data;
using GestionTrabajosDeGradoAPI.Interfaces;
using GestionTrabajosDeGradoAPI.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GestionTrabajosDeGradoAPI.Repository
{
    public class AutenticacionRepository : IAutenticacionRepository
    {

        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public AutenticacionRepository(AppDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;

        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var usuario = await _context.usuarios
                .FirstOrDefaultAsync(u => u.correo == request.Correo);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Contraseña, usuario.contraseña))
            {
                throw new UnauthorizedAccessException("Correo o contraseña incorrectos");
            }

            usuario.fecha_ultimo_ingreso = DateTime.UtcNow;
            _context.usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            // Obtener roles asociados al usuario
            var roles = new List<string>();

            if (await _context.estudiantes.AnyAsync(e => e.id_usuario == usuario.id))
                roles.Add("Estudiante");

            if (await _context.asesors.AnyAsync(a => a.id_usuario == usuario.id))
                roles.Add("Asesor");

            if (await _context.directors.AnyAsync(d => d.id_usuario == usuario.id))
                roles.Add("Director");

            if (await _context.jurados.AnyAsync(j => j.id_usuario == usuario.id))
                roles.Add("Jurado");

            var token = _jwtService.GenerateToken(usuario.nombre, roles, usuario.id);

            return new LoginResponse
            {
                Nombre = usuario.nombre,
                Roles = roles,
                Token = token
            };
        }


    }
}
