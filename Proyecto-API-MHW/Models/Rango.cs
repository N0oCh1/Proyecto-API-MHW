using System;
using System.Collections.Generic;

namespace Proyecto_API_MHW.Models;

public partial class Rango
{
    public int IdRango { get; set; }

    public string? Rango1 { get; set; }

    public virtual ICollection<MonstroGrande> IdMonstros { get; set; } = new List<MonstroGrande>();
}
