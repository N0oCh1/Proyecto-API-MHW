using System;
using System.Collections.Generic;

namespace Proyecto_API_MHW.Models;

public partial class Item
{
    public int IdItem { get; set; }

    public int? IdMonstro { get; set; }

    public string? NombreItem { get; set; }

    public string? DescripcionItem { get; set; }

    public virtual MonstroGrande? IdMonstroNavigation { get; set; }
}
