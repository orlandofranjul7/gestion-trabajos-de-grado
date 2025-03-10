using GestionTrabajosDeGradoAPI.Models;
using GestionTrabajosDeGradoAPI.ViewModels;
using System.Security.Claims;

namespace GestionTrabajosDeGradoAPI.Interfaces
{
    public interface IPropuestasService
    {
        Task<List<propuesta>> GetAllAsync();
        Task<propuesta> GetByIdAsync(int id);
        Task<PropuestaResponseDetails> GetByIdDetailsAsync(int id);
        Task<List<propuesta>> GetPropuestasPerUsers(int idUsuario, ClaimsPrincipal user);
        Task<PropuestaResponse> AddAsync(PropuestaRequest request, ClaimsPrincipal user);
        Task<PropuestaResponse> ModificarPropuesta(PropuestaRequest request, ClaimsPrincipal user);
        Task<bool> EliminarPropuestaAsync(int id, ClaimsPrincipal user);
    }
}
