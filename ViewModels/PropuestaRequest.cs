namespace GestionTrabajosDeGradoAPI.ViewModels
{
    public class PropuestaRequest
    {
        public string Id { get; set; }
        public string TipoTrabajo { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public int IdInvestigacion { get; set; }
    }

}
