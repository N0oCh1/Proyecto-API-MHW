namespace Proyecto_API_MHW.DataClass
{
    public class DtoMonstroPreview
    {
        public int idMonstro { get; set; }
        public string nombre { get; set; }
        public List<DtoImagen> imagen { get; set; }
        public string detalle { get; set; }
    }
}
