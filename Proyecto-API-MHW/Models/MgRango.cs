using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_API_MHW.Models
{
    [Table("mg_rango")]
    public class MgRango
    {
        public int id_rango { get; set; }
        public int id_monstro { get; set; }
    }
}
