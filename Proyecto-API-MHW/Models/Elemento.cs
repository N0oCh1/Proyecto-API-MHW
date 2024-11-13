using System;
using System.Collections.Generic;

namespace Proyecto_API_MHW.Models;

public partial class Elemento
{
    public int IdElemento { get; set; }

    public string? Elemento1 { get; set; }

    public virtual ICollection<MgDebilidade> MgDebilidades { get; set; } = new List<MgDebilidade>();

    public virtual ICollection<MonstroGrande> IdMonstros { get; set; } = new List<MonstroGrande>();
}
