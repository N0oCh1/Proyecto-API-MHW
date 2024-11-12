using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_API_MHW.Models
{
    [Table("items")]
    public class Item
    {
        [Key]
        public int id_item { get; set; }
        public int id_monstro { get; set; }
        public string nombre_item { get; set; }
        public string descripcion_item {  get; set; }   
    }
}
