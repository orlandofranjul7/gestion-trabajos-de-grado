using System;
using System.Collections.Generic;

namespace GestionTrabajosDeGradoAPI.Models;

public partial class escuela
{
    public int id { get; set; }

    public string? nombre { get; set; }

    public int? creditos { get; set; }

    public string? estado { get; set; }

    public int? id_facultad { get; set; }

    public virtual ICollection<director> directors { get; set; } = new List<director>();

    public virtual facultad? id_facultadNavigation { get; set; }

    public virtual ICollection<linea_investigacion> linea_investigacions { get; set; } = new List<linea_investigacion>();

    public virtual ICollection<usuario> usuarios { get; set; } = new List<usuario>();

    public virtual ICollection<usuario> id_usuarios { get; set; } = new List<usuario>();
}
