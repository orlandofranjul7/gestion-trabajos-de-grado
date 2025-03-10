using System;
using System.Collections.Generic;

namespace GestionTrabajosDeGradoAPI.Models;

public partial class asesor_trabajo
{
    public int id_asesor { get; set; }

    public int id_trabajo { get; set; }

    public string? rol_asesor { get; set; }

    public virtual asesor id_asesorNavigation { get; set; } = null!;

    public virtual trabajos_de_grado id_trabajoNavigation { get; set; } = null!;
}
