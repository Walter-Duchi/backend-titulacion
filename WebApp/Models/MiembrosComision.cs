using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class MiembrosComision
{
    public int Id { get; set; }

    public int MiembrosComisionId { get; set; }

    public int? CoordinadorComisionId { get; set; }

    public virtual MiembrosComision? CoordinadorComision { get; set; }

    public virtual ICollection<MiembrosComision> InverseCoordinadorComision { get; set; } = new List<MiembrosComision>();

    public virtual Usuario MiembrosComisionNavigation { get; set; } = null!;
}
