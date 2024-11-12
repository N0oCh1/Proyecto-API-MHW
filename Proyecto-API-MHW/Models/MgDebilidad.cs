using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_API_MHW.Models
{
    [Table("mg_debilidades")]
    public class MgDebilidad
    {
        [Key]
        public int id_elemento {  get; set; }
        public int id_monstro { get; set; }
        public double eficacia { get; set; }
    }
}
