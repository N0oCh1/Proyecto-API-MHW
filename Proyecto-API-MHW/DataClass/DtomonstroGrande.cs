using Proyecto_API_MHW.Models;

namespace Proyecto_API_MHW.DataClass
{
    public class DtomonstroGrande
    {
        public int id { get; set; }
        public string name { get; set; }
        public int health { get; set; }
        public string monsterClass { get; set; }
        public DtoImagen image { get;set; }
        public List<string> location { get; set; }
        public List<string> range { get; set; }
        public List<DtoElemento> elements { get; set; }
        public List<DtoDebilidad> weekness { get; set; }
        public List<DtoItem> items { get; set; }
    }
}
