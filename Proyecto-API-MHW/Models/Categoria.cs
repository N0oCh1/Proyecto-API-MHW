using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_API_MHW.Models
{
    [Table("categoria_monstro")]
    public class Categoria
    {
        [Key]
        public int id_tipo_monstro { get; set; }
        public string tipo { get; set; }

    }
}
