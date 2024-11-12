using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_API_MHW.Models
{
    [Table("rangos")]
    public class Rango
    {
        [Key]
        public int id_rango { get; set; }
        public string rango { get; set; }
    }
}
