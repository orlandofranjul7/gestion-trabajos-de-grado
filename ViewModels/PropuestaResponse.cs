namespace GestionTrabajosDeGradoAPI.ViewModels
{
    public class PropuestaResponse
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? Estado { get; set; }
        public DateTime? Fecha { get; set; }
        public int? IdDirector { get; set; }
    }

}
