using GestionTrabajosDeGradoAPI.ViewModels;

namespace GestionTrabajosDeGradoAPI.Interfaces
{
    public interface IAutenticacionRepository
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
