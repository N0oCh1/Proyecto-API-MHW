using Proyecto_API_MHW.Models;

namespace Proyecto_API_MHW.DataClass
{
    public class DtomonstroGrande
    {
        public int id { get; set; }
        public string name { get; set; }
        public int health { get; set; }
        public DtoCategoria monsterClass { get; set; }
        public List<DtoImagen>image { get;set; }
        public List<DtoBioma> location { get; set; }
        public List<DtoRango> range { get; set; }
        public List<DtoElemento> elements { get; set; }
        public List<DtoDebilidad> weekness { get; set; }
        public List<DtoItem> items { get; set; }
    }
}
