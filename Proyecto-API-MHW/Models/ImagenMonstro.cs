using System;
using System.Collections.Generic;

namespace Proyecto_API_MHW.Models;

public partial class ImagenMonstro
{
    public int IdImagen { get; set; }

    public int? IdMonstro { get; set; }

    public string? IconUrl { get; set; }

    public string? ImageUrl { get; set; }

    public virtual MonstroGrande? IdMonstroNavigation { get; set; }
}
