using GestionTrabajosDeGradoAPI.Data;
using GestionTrabajosDeGradoAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionTrabajosDeGradoAPI.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;    
        }

        public async Task<string> ObtenerRolUsuario(int idUsuario)
        {
            if (_context.estudiantes.Any(e => e.id_usuario == idUsuario))
                return "Estudiante";

            // Verificar si es asesor
            if (await _context.asesors.AnyAsync(a => a.id_usuario == idUsuario))
                return "Asesor";

            // Verificar si es director
            if (await _context.directors.AnyAsync(d => d.id_usuario == idUsuario))
                return "Director";

            // Verificar si es jurado
            if (await _context.jurados.AnyAsync(j => j.id_usuario == idUsuario))
                return "Jurado";

            return "Usuario general";

        }
    }
}
