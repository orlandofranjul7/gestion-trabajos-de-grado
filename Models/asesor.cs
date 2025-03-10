using System;
using System.Collections.Generic;

namespace GestionTrabajosDeGradoAPI.Models;

public partial class asesor
{
    public int id { get; set; }

    public string? disponibilidad { get; set; }

    public int? id_usuario { get; set; }

    public virtual ICollection<asesor_trabajo> asesor_trabajos { get; set; } = new List<asesor_trabajo>();

    public virtual usuario? id_usuarioNavigation { get; set; }

    public virtual ICollection<especializacion> id_especializacions { get; set; } = new List<especializacion>();
}
