using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_API_MHW.Models
{
    [Table("monstro_grande")]
    public class MonstroGrande
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_monstrog { get; set; }
        public string nombre { get; set; }
        public int vida { get; set; }
        public int id_categoria { get; set; }

    }
}
