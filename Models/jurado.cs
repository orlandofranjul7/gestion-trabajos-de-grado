using System;
using System.Collections.Generic;

namespace GestionTrabajosDeGradoAPI.Models;

public partial class jurado
{
    public int id { get; set; }

    public string? disponibilidad { get; set; }

    public int id_usuario { get; set; }

    public virtual usuario id_usuarioNavigation { get; set; } = null!;

    public virtual ICollection<trabajos_de_grado> id_trabajos { get; set; } = new List<trabajos_de_grado>();
}
