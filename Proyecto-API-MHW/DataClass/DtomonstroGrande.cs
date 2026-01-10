using Proyecto_API_MHW.Models;

namespace Proyecto_API_MHW.DataClass
{

    public class DtomonstroGrande
    {
        public int idMonstro { get; set; }

        // Campos de texto obligatorios: inicia en string.Empty
        public string nombre { get; set; } = string.Empty;
        public int vida { get; set; }
        public string descripcion { get; set; } = string.Empty;

        // Objetos anidados: inicializa para evitar dereferencias nulas
        public DtoCategoria tipo { get; set; } = new DtoCategoria();
        public DtoImagen imagen { get; set; } = new DtoImagen();

        // Colecciones: inicializa en listas vacías
        public List<DtoBioma> biomas { get; set; } = new();
        public List<DtoRango> rangos { get; set; } = new();
        public List<DtoElemento> elementos { get; set; } = new();
        public List<DtoDebilidad> debilidad { get; set; } = new();
        public List<DtoItem> items { get; set; } = new();

    }
}