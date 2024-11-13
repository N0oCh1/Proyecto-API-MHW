using System;
using System.Collections.Generic;

namespace Proyecto_API_MHW.Models;

public partial class MgDebilidade
{
    public int IdElemento { get; set; }

    public int IdMonstro { get; set; }

    public double? Eficacia { get; set; }

    public virtual Elemento IdElementoNavigation { get; set; } = null!;

    public virtual MonstroGrande IdMonstroNavigation { get; set; } = null!;
}
