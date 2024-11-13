using System;
using System.Collections.Generic;

namespace Proyecto_API_MHW.Models;

public partial class MonstroGrande
{
    public int IdMonstrog { get; set; }

    public string? Nombre { get; set; }

    public int? Vida { get; set; }

    public int? IdImagen { get; set; }

    public int? IdCategoria { get; set; }

    public virtual CategoriaMonstro? IdCategoriaNavigation { get; set; }

    public virtual ImagenMonstro? IdImagenNavigation { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual ICollection<MgDebilidade> MgDebilidades { get; set; } = new List<MgDebilidade>();

    public virtual ICollection<Bioma> IdBiomas { get; set; } = new List<Bioma>();

    public virtual ICollection<Elemento> IdElementos { get; set; } = new List<Elemento>();

    public virtual ICollection<Rango> IdRangos { get; set; } = new List<Rango>();
}
