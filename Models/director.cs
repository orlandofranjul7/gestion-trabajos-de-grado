using System;
using System.Collections.Generic;

namespace GestionTrabajosDeGradoAPI.Models;

public partial class director
{
    public int id { get; set; }

    public int id_escuela { get; set; }

    public int id_usuario { get; set; }

    public virtual escuela id_escuelaNavigation { get; set; } = null!;

    public virtual usuario id_usuarioNavigation { get; set; } = null!;

    public virtual ICollection<propuesta> propuesta { get; set; } = new List<propuesta>();
}
