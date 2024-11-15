using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_API_MHW.Models
{
    [Table("mg_bioma")]
    public class MgBioma
    {
        public int id_bioma {  get; set; }
        public int id_monstro {  get; set; }
    }
}
