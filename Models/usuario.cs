using System;
using System.Collections.Generic;

namespace GestionTrabajosDeGradoAPI.Models;

public partial class usuario
{
    public int id { get; set; }

    public string? nombre { get; set; }

    public string? contraseña { get; set; }

    public string? correo { get; set; }

    public DateTime? fecha_ingreso { get; set; }

    public string? imagen { get; set; }

    public string? genero { get; set; }

    public DateTime? fecha_ultimo_ingreso { get; set; }

    public string? estado { get; set; }

    public string? telefono { get; set; }

    public string? direccion { get; set; }

    public int? id_escuela { get; set; }

    public virtual ICollection<asesor> asesors { get; set; } = new List<asesor>();

    public virtual ICollection<director> directors { get; set; } = new List<director>();

    public virtual ICollection<estudiante> estudiantes { get; set; } = new List<estudiante>();

    public virtual ICollection<evento> eventos { get; set; } = new List<evento>();

    public virtual ICollection<historial_de_cambio> historial_de_cambios { get; set; } = new List<historial_de_cambio>();

    public virtual escuela? id_escuelaNavigation { get; set; }

    public virtual ICollection<jurado> jurados { get; set; } = new List<jurado>();

    public virtual ICollection<escuela> id_escuelas { get; set; } = new List<escuela>();
}
