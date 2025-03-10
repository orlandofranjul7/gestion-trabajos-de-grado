using GestionTrabajosDeGradoAPI.Models;

namespace GestionTrabajosDeGradoAPI.Interfaces
{
    public interface ITrabajosDeGradoRepository
    {
        Task<IEnumerable<trabajos_de_grado>> GetAll();
        Task<trabajos_de_grado> GetByIdAsync(int id);
        Task<trabajos_de_grado> GetByPropuesta(int id);
        Task<IEnumerable<trabajos_de_grado>> getByIdEstudiante(int id);
        Task<IEnumerable<trabajos_de_grado>> GetByAsesor(int id);
        Task<IEnumerable<trabajos_de_grado>> GetTrabajoByDate(DateTime fechaInicio, DateTime fechaFin);
        bool Add(trabajos_de_grado trabajosDeGrado);
        bool Update(trabajos_de_grado trabajosDeGrado);
        bool Delete(trabajos_de_grado trabajosDeGrado);
        bool Save();
    }
}
