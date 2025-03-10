using System;
using System.Collections.Generic;

namespace GestionTrabajosDeGradoAPI.Models;

public partial class trabajos_de_grado
{
    public int id { get; set; }

    public string titulo { get; set; } = null!;

    public string descripcion { get; set; } = null!;

    public string objetivo_general { get; set; } = null!;

    public string objetivos_especificos { get; set; } = null!;

    public string justificacion { get; set; } = null!;

    public byte progreso { get; set; }

    public string planteamiento { get; set; } = null!;

    public string? anteproyecto { get; set; }

    public string? trabajo { get; set; }

    public string? estado { get; set; }

    public DateTime fecha_inicio { get; set; }

    public DateTime fecha_fin { get; set; }

    public int? id_propuesta { get; set; }

    public virtual ICollection<asesor_trabajo> asesor_trabajos { get; set; } = new List<asesor_trabajo>();

    public virtual ICollection<evento> eventos { get; set; } = new List<evento>();

    public virtual ICollection<historial_de_cambio> historial_de_cambios { get; set; } = new List<historial_de_cambio>();

    public virtual propuesta? id_propuestaNavigation { get; set; }

    public virtual ICollection<estudiante> id_estudiantes { get; set; } = new List<estudiante>();

    public virtual ICollection<jurado> id_jurados { get; set; } = new List<jurado>();
}
