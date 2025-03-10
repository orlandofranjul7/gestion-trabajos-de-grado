namespace GestionTrabajosDeGradoAPI.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<string> ObtenerRolUsuario(int idUsuario);
    }
}
