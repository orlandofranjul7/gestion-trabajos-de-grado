using GestionTrabajosDeGradoAPI.Data;
using GestionTrabajosDeGradoAPI.Interfaces;
using GestionTrabajosDeGradoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionTrabajosDeGradoAPI.Repository
{

    public class LineaInvestigacionRepository : ILineaInvestigacionRepository
    {

        private readonly AppDbContext _context;

        public LineaInvestigacionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<linea_investigacion>> getInvestigacionPerEscuela(int IdUsuario)
        {
            var estudiante = await _context.estudiantes
                .Include(e => e.id_usuarioNavigation)
                .FirstOrDefaultAsync(e => e.id_usuario == IdUsuario);

            if(estudiante == null) 
            {
                throw new Exception("El usuario no es un estudiante");
            }

            return await _context.linea_investigacions
                .Where(i => i.id_escuela == estudiante.id_usuarioNavigation.id_escuela)
                .ToListAsync();

        }
    }
}
