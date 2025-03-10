using GestionTrabajosDeGradoAPI.Data;
using GestionTrabajosDeGradoAPI.Interfaces;
using GestionTrabajosDeGradoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionTrabajosDeGradoAPI.Repository
{
    public class TrabajosDeGradoRepository : ITrabajosDeGradoRepository
    {
        private readonly AppDbContext _context;

        public TrabajosDeGradoRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool Add(trabajos_de_grado trabajosDeGrado)
        {
            _context.Add(trabajosDeGrado);
            return Save();
        }

        public bool Delete(trabajos_de_grado trabajosDeGrado)
        {
            trabajosDeGrado.estado = "Inactivo";
            return Save();
        }

        public async Task<IEnumerable<trabajos_de_grado>> GetAll()
        {
            return await _context.trabajos_de_grados
                .Include(t => t.id_estudiantes)
                .Include(t => t.id_jurados)
                .Include(t => t.asesor_trabajos)
                .Include(t => t.id_propuestaNavigation)
                .ToListAsync();
        }


        public Task<IEnumerable<trabajos_de_grado>> GetByAsesor(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<trabajos_de_grado> GetByIdAsync(int id)
        {
            return await _context.trabajos_de_grados
                .Include(t => t.id_estudiantes)
                .Include(t => t.asesor_trabajos)
                .ThenInclude(at => at.id_asesor)
                .FirstOrDefaultAsync(t => t.id == id);
                
        }

        public async Task<IEnumerable<trabajos_de_grado>> getByIdEstudiante(int id)
        {
            return await _context.trabajos_de_grados
                .Include(t => t.id_estudiantes) // Carga los estudiantes asociados
                .Where(t => t.id_estudiantes.Any(e => e.id_usuario == id)) // Verifica que el estudiante esté asociado
                .ToListAsync();
        }

        public Task<trabajos_de_grado> GetByPropuesta(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<trabajos_de_grado>> GetTrabajoByDate(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _context.trabajos_de_grados
                .Where(t => t.fecha_inicio >= fechaInicio && t.fecha_fin <= fechaFin)
                .ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(trabajos_de_grado trabajosDeGrado)
        {
            _context.Update(trabajosDeGrado);
            return Save();
        }
    }
}
