using System;
using System.Collections.Generic;

namespace GestionTrabajosDeGradoAPI.Models;

public partial class linea_investigacion
{
    public int id { get; set; }

    public string? nombre { get; set; }

    public string? estado { get; set; }

    public int? id_escuela { get; set; }

    public virtual escuela? id_escuelaNavigation { get; set; }

    public virtual ICollection<propuesta> propuesta { get; set; } = new List<propuesta>();
}
