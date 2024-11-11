using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_API_MHW.Models
{
    [Table("v_monstro_grande")]
    public class vMonstroGrandes
    {
        [Key]
        [Display(Name = "id")]
        public int id_monstrog { get; set; }
        [Display(Name = "name")]
        public string nombre { get; set; }
        [Display(Name = "health")]
        public int vida { get; set; }
        [Display(Name = "monsterClass")]
        public string tipo { get; set; }
    }
}
