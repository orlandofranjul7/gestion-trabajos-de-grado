using System;
using System.Collections.Generic;

namespace GestionTrabajosDeGradoAPI.Models;

public partial class historial_de_cambio
{
    public int id { get; set; }

    public string? titulo { get; set; }

    public DateTime? fecha { get; set; }

    public string? descripcion { get; set; }

    public int? id_trabajo { get; set; }

    public int? id_propuesta { get; set; }

    public int id_autor { get; set; }

    public virtual usuario id_autorNavigation { get; set; } = null!;

    public virtual propuesta? id_propuestaNavigation { get; set; }

    public virtual trabajos_de_grado? id_trabajoNavigation { get; set; }
}
