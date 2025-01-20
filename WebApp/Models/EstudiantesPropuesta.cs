using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class EstudiantesPropuesta
{
    public int Id { get; set; }

    public int EstudianteId { get; set; }

    public int PropuestaId { get; set; }

    public virtual Usuario Estudiante { get; set; } = null!;

    public virtual Propuesta Propuesta { get; set; } = null!;
}
