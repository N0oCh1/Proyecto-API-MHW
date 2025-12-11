using System;
using System.Collections.Generic;

namespace Proyecto_API_MHW.Models;

public partial class Usuario
{
    public int Idusuario { get; set; }

    public string? Nombreusuario { get; set; }

    public string? Password { get; set; }

    // Nuevas columnas para almacenar correo, nombre y apellido
    public string? Correo { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }
}
