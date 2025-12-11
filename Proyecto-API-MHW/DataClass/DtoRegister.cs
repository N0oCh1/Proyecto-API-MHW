using System.ComponentModel.DataAnnotations;

namespace Proyecto_API_MHW.DataClass
{
    public class DtoRegister
    {
        [Required]
        [StringLength(50)]
        public string usuario { get; set; } = null!;

        [Required]
        [StringLength(50)]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        public string password { get; set; } = null!;

        [Required]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Correo inválido.")]
        public string correo { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string nombre { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string apellido { get; set; } = null!;
    }
}
