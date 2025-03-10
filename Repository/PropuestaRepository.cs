using GestionTrabajosDeGradoAPI.Data;
using GestionTrabajosDeGradoAPI.Helpers;
using GestionTrabajosDeGradoAPI.Interfaces;
using GestionTrabajosDeGradoAPI.Models;
using GestionTrabajosDeGradoAPI.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GestionTrabajosDeGradoAPI.Repository
{
    public class PropuestaRepository : IPropuestasService
    {

        private readonly AppDbContext _context;
        private readonly IUsuarioRepository _usuarioRepository;

        public PropuestaRepository(AppDbContext context, IUsuarioRepository usuarioRepository)
        {
            _context = context;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<PropuestaResponse> AddAsync(PropuestaRequest request, ClaimsPrincipal user)
        {
            // Obtener el ID del usuario desde el token
            int idUsuario = TokenHelper.GetUserIdToken(user);

            // Obtener al estudiante relacionado con este usuario
            var estudiante = await _context.estudiantes
                .Include(e => e.id_usuarioNavigation)
                .FirstOrDefaultAsync(e => e.id_usuario == idUsuario);

            if (estudiante == null)
                throw new Exception("No se encontró el estudiante asociado al usuario.");

            // Verificar si el estudiante tiene una escuela asociada
            if (estudiante.id_usuarioNavigation.id_escuela == null)
                throw new Exception("El estudiante no tiene una escuela asociada.");

            // Obtener el director asociado a la escuela del estudiante
            var director = await _context.directors
                .FirstOrDefaultAsync(d => d.id_escuela == estudiante.id_usuarioNavigation.id_escuela);

            if (director == null)
                throw new Exception("No se encontró un director para la escuela del estudiante.");

            // Crear la nueva propuesta
            var nuevaPropuesta = new propuesta
            {
                tipo_trabajo = request.TipoTrabajo,
                titulo = request.Titulo,
                descripcion = request.Descripcion,
                estado = "Pendiente", // Estado inicial
                fecha = DateTime.UtcNow,
                id_director = director.id,
                id_investigacion = request.IdInvestigacion
            };

            // Guardar la nueva propuesta en la base de datos
            _context.propuestas.Add(nuevaPropuesta);
            await _context.SaveChangesAsync();

            // Agregar la relación entre estudiante y propuesta
            estudiante.id_propuesta.Add(nuevaPropuesta); // Relación implícita
            await _context.SaveChangesAsync();

            // Retornar la respuesta
            return new PropuestaResponse
            {
                Titulo = nuevaPropuesta.titulo,
                Descripcion = nuevaPropuesta.descripcion,
                Estado = nuevaPropuesta.estado
            };
        }

        public async Task<bool> EliminarPropuestaAsync(int id, ClaimsPrincipal user)
        {
            // Obtener el ID del usuario desde el token JWT
            int idUsuario = TokenHelper.GetUserIdToken(user);

            var propuesta = await _context.propuestas.FindAsync(id);
            if (propuesta == null)
            {
                throw new KeyNotFoundException("No se encontró la propuesta especificada.");
            }

            // Cambiar el estado a "Eliminado"
            var estadoAnterior = propuesta.estado;
            propuesta.estado = "Eliminado";
            _context.propuestas.Update(propuesta);
            await _context.SaveChangesAsync();

            // Registrar el cambio en el historial
            var historial = new historial_de_cambio
            {
                titulo = "Eliminación de propuesta",
                descripcion = $"La propuesta con ID {propuesta.id} cambió de estado: {estadoAnterior} -> Eliminado.",
                fecha = DateTime.UtcNow,
                id_propuesta = propuesta.id,
                id_autor = idUsuario
            };
            _context.historial_de_cambios.Add(historial);
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<List<propuesta>> GetAllAsync()
        {
            return await _context.propuestas
                .Where(p => p.estado != "Rechazado" && p.estado != "Eliminado")
                .Include(p => p.id_directorNavigation)
                .ToListAsync();
        }


        
        public async Task<propuesta> GetByIdAsync(int id)
        {
            var propuesta =  await _context.propuestas
                .Where(p => p.estado != "Rechazado" && p.estado != "Eliminado")
                .Include(p => p.id_estudiantes)
                .Include(p => p.id_investigacionNavigation)
                .Include(p => p.id_directorNavigation)
                .FirstOrDefaultAsync(p => p.id == id);

            return propuesta;
        }



        public async Task<PropuestaResponseDetails> GetByIdDetailsAsync(int id)
        {
            var propuesta = await _context.propuestas
                .Where(p => p.id == id && p.estado != "Rechazado" && p.estado != "Eliminado")
                .Include(p => p.id_investigacionNavigation)
                .Include(p => p.id_directorNavigation)
                .ThenInclude(d => d.id_usuarioNavigation) // 🔹 Asegurar que se carga el nombre del director
                .Include(p => p.id_estudiantes)
                .ThenInclude(e => e.id_usuarioNavigation) // 🔹 Asegurar que se carga el nombre de los sustentantes
                .AsNoTracking() // 🔹 Evita problemas de tracking de Entity Framework
                .FirstOrDefaultAsync();

            if (propuesta == null)
                return null;

            return new PropuestaResponseDetails
            {
                Id = propuesta.id,
                TipoTrabajo = propuesta.tipo_trabajo,
                Titulo = propuesta.titulo,
                Descripcion = propuesta.descripcion,
                Estado = propuesta.estado,
                Fecha = propuesta.fecha,

                // ✅ Obtener el nombre del director
                Director = propuesta.id_directorNavigation?.id_usuarioNavigation?.nombre ?? "No especificado",

                // ✅ Obtener los nombres de los sustentantes (estudiantes)
                Sustentantes = propuesta.id_estudiantes
                    .Where(e => e.id_usuarioNavigation != null) // 🔹 Evitar valores null
                    .Select(e => e.id_usuarioNavigation.nombre)
                    .ToList(),

                // ✅ Obtener el nombre de la línea de investigación
                LineaInvestigacion = propuesta.id_investigacionNavigation?.nombre ?? "No especificada"
            };
        }




        public async Task<List<propuesta>> GetPropuestasPerUsers(int idUsuario, ClaimsPrincipal user)
        {
            /*
            var estudiante = await _context.estudiantes.FirstOrDefaultAsync(e => e.id == id);

            if(estudiante != null) 
            {
                return await _context.propuestas
                    .Include(p => p.id_investigacionNavigation)
                    .Include(p => p.id_director)
                    .Where(p => p.id_estudiantes.Any(ep => ep.id == estudiante.id) && p.estado != "Rechazado" && p.estado != "Eliminado")
                    .ToListAsync();
            }

            // En caso de que el usuario sea un director

            var director = await _context.directors.FirstOrDefaultAsync(d => d.id == id);

            if (director != null) 
            {
                return await _context.propuestas
                    .Include(p => p.id_investigacionNavigation)
                    .Include(p => p.id_estudiantes)
                    .Where(p => p.id_director == director.id && p.estado != "Rechazado" && p.estado != "Eliminado")
                    .ToListAsync();
            }

            // En caso de no ser ni direcor ni estudiante

            return (List<propuesta>)Enumerable.Empty<propuesta>();
            */


            // Obtener roles desde el JWT
            var roles = user.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            // Si el usuario tiene el rol de "Estudiante"
            if (roles.Contains("Estudiante"))
            {
                var estudiante = await _context.estudiantes.FirstOrDefaultAsync(e => e.id_usuario == idUsuario);

                if (estudiante != null)
                {
                    return await _context.propuestas
                        .Include(p => p.id_investigacionNavigation)
                        .Include(p => p.id_directorNavigation)
                        .Include(p => p.id_estudiantes) // Asegura que se carguen los estudiantes
                        .ThenInclude(e => e.id_usuarioNavigation) // Incluye los datos del usuario del estudiante
                        .Where(p => p.id_estudiantes.Any(ep => ep.id == estudiante.id) &&
                                    p.estado != "Rechazado" && p.estado != "Eliminado")
                        .ToListAsync(); 

                }
            }

            // Si el usuario tiene el rol de "Director"
            if (roles.Contains("Director"))
            {
                var director = await _context.directors.FirstOrDefaultAsync(d => d.id_usuario == idUsuario);

                if (director != null)
                {
                    return await _context.propuestas
                        .Include(p => p.id_investigacionNavigation)
                        .Include(p => p.id_estudiantes)
                        .Where(p => p.id_director == director.id &&
                                    p.estado != "Rechazado" && p.estado != "Eliminado")
                        .ToListAsync();
                }
            }

            // Si el rol no es "Estudiante" o "Director"
            return new List<propuesta>();


        }

        public async Task<PropuestaResponse> ModificarPropuesta(PropuestaRequest request, ClaimsPrincipal user)
        {
            // Obtener el ID del usuario desde el token JWT
            int idUsuario = TokenHelper.GetUserIdToken(user);

            // Verificar si la propuesta existe
            var propuesta = await _context.propuestas
                .Include(p => p.id_estudiantes) // Incluye los estudiantes relacionados
                .Include(p => p.id_directorNavigation) // Incluye el director relacionado
                .FirstOrDefaultAsync(p => p.id == request.Id);

            if (propuesta == null)
                throw new KeyNotFoundException("No se encontró la propuesta.");

            // Verificar si el usuario es el director o un estudiante asociado
            bool esDirector = propuesta.id_directorNavigation.id_usuario == idUsuario;
            bool esEstudiante = propuesta.id_estudiantes.Any(e => e.id_usuario == idUsuario);

            if (!esDirector && !esEstudiante)
                throw new UnauthorizedAccessException("No tienes permisos para modificar esta propuesta.");

            // Verificar restricciones para los roles
            if (esEstudiante && request.Estado != null)
                throw new UnauthorizedAccessException("Un estudiante no puede modificar el estado de una propuesta.");

            // Actualizar solo los campos que vienen en el request (si no están, se mantienen los actuales)
            propuesta.titulo = request.Titulo ?? propuesta.titulo;
            propuesta.descripcion = request.Descripcion ?? propuesta.descripcion;
            propuesta.tipo_trabajo = request.TipoTrabajo ?? propuesta.tipo_trabajo;
            propuesta.estado = esDirector ? (request.Estado ?? propuesta.estado) : propuesta.estado;

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            // Retornar una respuesta con los datos actualizados
            return new PropuestaResponse
            {
                Id = propuesta.id,
                Titulo = propuesta.titulo,
                Descripcion = propuesta.descripcion,
                Estado = propuesta.estado
            };
        }




    }
}
