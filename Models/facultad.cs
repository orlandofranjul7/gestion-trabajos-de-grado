using System;
using System.Collections.Generic;

namespace GestionTrabajosDeGradoAPI.Models;

public partial class facultad
{
    public int id { get; set; }

    public string? nombre { get; set; }

    public string? estado { get; set; }

    public virtual ICollection<escuela> escuelas { get; set; } = new List<escuela>();
}
