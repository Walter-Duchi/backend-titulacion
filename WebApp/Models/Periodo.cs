using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class Periodo
{
    public int Id { get; set; }

    public bool? Estado { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public string? CicloActual { get; set; }
}
