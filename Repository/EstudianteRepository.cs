using GestionTrabajosDeGradoAPI.Data;
using GestionTrabajosDeGradoAPI.Interfaces;
using GestionTrabajosDeGradoAPI.Models;
using GestionTrabajosDeGradoAPI.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GestionTrabajosDeGradoAPI.Repository
{
    public class EstudianteRepository : IEstudianteRepository
    {

        private readonly AppDbContext _context;

        public EstudianteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EstudianteResponse>> GetEstudiantesPorEscuelaAsync(int idUsuario)
        {
            // Obtener el estudiante asociado al usuario
            var estudiante = await _context.estudiantes
                .Include(e => e.id_usuarioNavigation)
                .FirstOrDefaultAsync(e => e.id_usuario == idUsuario);

            if (estudiante == null || estudiante.id_usuarioNavigation.id_escuela == null)
                return new List<EstudianteResponse>(); // Retorna lista vacía si no se encuentra

            // Obtener estudiantes de la misma escuela
            return await _context.estudiantes
                .Include(e => e.id_usuarioNavigation)
                .Where(e => e.id_usuarioNavigation.id_escuela == estudiante.id_usuarioNavigation.id_escuela)
                .Select(e => new EstudianteResponse
                {
                    Id = e.id,
                    Nombre = e.id_usuarioNavigation.nombre,
                    Correo = e.id_usuarioNavigation.correo
                })
                .ToListAsync();
        }
    }
}
