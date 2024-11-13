using System;
using System.Collections.Generic;

namespace Proyecto_API_MHW.Models;

public partial class Debilidade
{
    public int Id { get; set; }

    public int? IdElemento { get; set; }

    public int? IdMonstro { get; set; }

    public double? Eficacia { get; set; }
}
