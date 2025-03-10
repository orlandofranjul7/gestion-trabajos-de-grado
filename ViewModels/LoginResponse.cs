
namespace GestionTrabajosDeGradoAPI.ViewModels
{
    public class LoginResponse
    {
        public string Nombre { get; set; }
        public bool PuedeProponerTrabajos { get; set; }
        public string Token { get; set; } // Para autenticación JWT 
        public List<string> Roles { get; internal set; }
    }

}
