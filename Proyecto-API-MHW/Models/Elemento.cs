using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_API_MHW.Models
{
    [Table("elementos")]
    public class Elemento
    {
        [Key]
        public int id_elemento {  get; set; }
        public string elemento { get; set; }    
    }
}
