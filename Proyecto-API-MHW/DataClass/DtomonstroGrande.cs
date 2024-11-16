using Proyecto_API_MHW.Models;

namespace Proyecto_API_MHW.DataClass
{
    public class DtomonstroGrande
    {
        public int idMonstro { get; set; }
        public string nombre { get; set; }
        public int vida { get; set; }
        public DtoCategoria tipo { get; set; }
        public DtoImagen imagen { get;set; }
        public List<DtoBioma> biomas { get; set; }
        public List<DtoRango> rangos { get; set; }
        public List<DtoElemento> elementos { get; set; }
        public List<DtoDebilidad> debilidad { get; set; }
        public List<DtoItem> items { get; set; }
    }
}
