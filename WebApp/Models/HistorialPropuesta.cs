using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class HistorialPropuesta
{
    public int Id { get; set; }

    public string DireccionArchivo { get; set; } = null!;

    public DateTime FechaEnvio { get; set; }

    public bool? EstadoAprobacion { get; set; }

    public string? ObservacionRevisor { get; set; }

    public string? ComentarioEstudiante { get; set; }

    public int PropuestaId { get; set; }

    public virtual Propuesta Propuesta { get; set; } = null!;
}
