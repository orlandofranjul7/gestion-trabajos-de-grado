using GestionTrabajosDeGradoAPI.Models;

namespace GestionTrabajosDeGradoAPI.Interfaces
{
    public interface ILineaInvestigacionRepository
    {
        Task<List<linea_investigacion>> getInvestigacionPerEscuela(int IdUsuario);
    }
}
