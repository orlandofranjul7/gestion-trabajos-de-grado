using System;
using System.Collections.Generic;

namespace GestionTrabajosDeGradoAPI.Models;

public partial class evento
{
    public int id { get; set; }

    public string titulo { get; set; } = null!;

    public DateTime fecha { get; set; }

    public string? descripcion { get; set; }

    public int id_trabajo { get; set; }

    public int id_usuario { get; set; }

    public virtual trabajos_de_grado id_trabajoNavigation { get; set; } = null!;

    public virtual usuario id_usuarioNavigation { get; set; } = null!;
}
