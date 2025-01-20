using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public byte Rol { get; set; }

    public virtual ICollection<Comisione> Comisiones { get; set; } = new List<Comisione>();

    public virtual EstudiantesPropuesta? EstudiantesPropuesta { get; set; }

    public virtual ICollection<MiembrosComision> MiembrosComisions { get; set; } = new List<MiembrosComision>();

    public virtual ICollection<Propuesta> Propuesta { get; set; } = new List<Propuesta>();
}
