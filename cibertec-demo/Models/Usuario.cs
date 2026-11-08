using System.ComponentModel.DataAnnotations;

namespace cibertec_demo.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")] // para campo obligatorio
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres")] // longitud máxima
        [Display(Name = "Nombre completo")] // nombre a mostrar en el formulario
        public string nombre { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [StringLength(50, ErrorMessage = "El correo no puede superar los 50 caracteres")]
        [EmailAddress(ErrorMessage = "Correo electrónico inválido")] // valida que sea correo electrónico
        [Display(Name = "Correo electrónico")]
        public string correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "La contraseña debe tener entre 4 y 50 caracteres")] // longitud máxima y mínima
        [DataType(DataType.Password)] // oculta el texto y muestra ••••
        [Display(Name = "Contraseña")]
        public string clave { get; set; }

        [Display(Name = "Fecha de registro")]
        public DateTime fechaRegistro { get; set; }
    }
}
