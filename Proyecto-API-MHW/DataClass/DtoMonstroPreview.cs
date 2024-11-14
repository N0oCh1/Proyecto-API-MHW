namespace Proyecto_API_MHW.DataClass
{
    public class DtoMonstroPreview
    {
        public int idMonstro { get; set; }
        public string name { get; set; }
        public List<DtoImagen> image { get; set; }
        public string url { get; set; }
    }
}
