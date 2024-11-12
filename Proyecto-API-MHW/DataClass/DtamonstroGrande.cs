using Proyecto_API_MHW.Models;

namespace Proyecto_API_MHW.DataClass
{
    public class DtamonstroGrande
    {
        public int id { get; set; }
        public string name { get; set; }
        public int health { get; set; }
        public string monsterClass { get; set; }
        public List<string> location { get; set; }
        public List<string> range { get; set; }
        public List<Elemento> elements { get; set; }
        public List<DtaDebilidad> weekness { get; set; }
        public List<DtaItem> items { get; set; }
    }
}
