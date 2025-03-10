using System;
using System.Collections.Generic;

namespace GestionTrabajosDeGradoAPI.Models;

public partial class propuesta
{
    public int id { get; set; }

    public string? tipo_trabajo { get; set; }

    public string titulo { get; set; } = null!;

    public string descripcion { get; set; } = null!;
    public DateTime? fecha { get; set; }

    public string? estado { get; set; }

    public int id_director { get; set; }

    public int id_investigacion { get; set; }

    public virtual ICollection<historial_de_cambio> historial_de_cambios { get; set; } = new List<historial_de_cambio>();

    public virtual director id_directorNavigation { get; set; } = null!;

    public virtual linea_investigacion id_investigacionNavigation { get; set; } = null!;

    public virtual ICollection<trabajos_de_grado> trabajos_de_grados { get; set; } = new List<trabajos_de_grado>();

    public virtual ICollection<estudiante> id_estudiantes { get; set; } = new List<estudiante>();
}
