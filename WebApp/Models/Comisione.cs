using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class Comisione
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Proposito { get; set; } = null!;

    public bool Estado { get; set; }

    public DateTime FechaCreación { get; set; }

    public int GestorId { get; set; }

    public virtual Usuario Gestor { get; set; } = null!;
}
