namespace Proyecto_API_MHW.DataClass
{
    public class DtoMonstroPreview
    {
        public int idMonstro { get; set; }
        public string nombre { get; set; }
        public List<DtoElemento> elementos { get; set; }
        public DtoImagen imagen { get; set; }
        public string detalle { get; set; }
    }
}
