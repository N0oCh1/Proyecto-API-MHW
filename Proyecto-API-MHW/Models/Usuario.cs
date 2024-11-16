using System;
using System.Collections.Generic;

namespace Proyecto_API_MHW.Models;

public partial class Usuario
{
    public int Idusuario { get; set; }

    public string? Nombreusuario { get; set; }

    public string? Password { get; set; }
}
