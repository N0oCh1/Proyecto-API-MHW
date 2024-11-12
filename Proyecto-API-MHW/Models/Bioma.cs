using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_API_MHW.Models
{

    [Table("biomas")]
    public class Bioma
    {
        [Key]
        public  int id_bioma { get; set; }
        public string nombre_bioma { get; set; }

    }
}
