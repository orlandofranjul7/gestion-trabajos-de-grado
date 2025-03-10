using System;
using System.Collections.Generic;

namespace GestionTrabajosDeGradoAPI.Models;

public partial class estudiante
{
    public int id { get; set; }

    public string? matricula { get; set; }

    public int? creditos_aprobados { get; set; }

    public int? id_usuario { get; set; }

    public virtual usuario? id_usuarioNavigation { get; set; }

    public virtual ICollection<propuesta> id_propuesta { get; set; } = new List<propuesta>();

    public virtual ICollection<trabajos_de_grado> id_trabajos { get; set; } = new List<trabajos_de_grado>();
}
