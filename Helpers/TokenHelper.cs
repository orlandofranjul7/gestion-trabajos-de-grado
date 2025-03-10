using System.Security.Claims;

namespace GestionTrabajosDeGradoAPI.Helpers
{
    public static class TokenHelper
    {
        // Helper para obtener el id de los usuarios de los JWT
        public static int GetUserIdToken(ClaimsPrincipal user)
        {
            var claimsIdentity = user.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var idUsuarioClaim = claimsIdentity.FindFirst("UsuarioId");
                if (idUsuarioClaim != null && int.TryParse(idUsuarioClaim.Value, out var idUsuario))
                {
                    return idUsuario;
                }
            }
            throw new UnauthorizedAccessException("No se pudo obtener el ID del usuario");
        }
    }
}