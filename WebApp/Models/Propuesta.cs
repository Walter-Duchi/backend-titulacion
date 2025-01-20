using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class Propuesta
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public DateTime FechaCreación { get; set; }

    public double? Calificacion { get; set; }

    public int RevisorId { get; set; }

    public virtual ICollection<EstudiantesPropuesta> EstudiantesPropuesta { get; set; } = new List<EstudiantesPropuesta>();

    public virtual ICollection<HistorialPropuesta> HistorialPropuesta { get; set; } = new List<HistorialPropuesta>();

    public virtual Usuario Revisor { get; set; } = null!;
}
