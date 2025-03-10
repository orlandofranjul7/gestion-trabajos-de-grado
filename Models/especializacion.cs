using System;
using System.Collections.Generic;

namespace GestionTrabajosDeGradoAPI.Models;

public partial class especializacion
{
    public int id { get; set; }

    public string? nombre { get; set; }

    public virtual ICollection<asesor> id_asesors { get; set; } = new List<asesor>();
}
