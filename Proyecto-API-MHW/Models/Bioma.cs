using System;
using System.Collections.Generic;

namespace Proyecto_API_MHW.Models;

public partial class Bioma
{
    public int IdBioma { get; set; }

    public string? NombreBioma { get; set; }

    public virtual ICollection<MonstroGrande> IdMonstros { get; set; } = new List<MonstroGrande>();
}
