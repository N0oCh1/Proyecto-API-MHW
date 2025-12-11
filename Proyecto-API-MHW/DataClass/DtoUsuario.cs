namespace Proyecto_API_MHW.DataClass
{
    public class DtoUsuario
    {
        public string usuario { get; set; }
        public string password { get; set; }

        // Nuevos campos para registro
        public string correo { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
    }
}
