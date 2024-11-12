using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_API_MHW.Models
{
    [Table("elemento_monstro")]
    public class MgElemento
    {
        public int id_elemento {  get; set; }
        public int id_monstro { get; set; }
    }
}
