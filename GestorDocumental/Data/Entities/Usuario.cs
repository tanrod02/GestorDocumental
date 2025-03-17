using System.ComponentModel.DataAnnotations;

namespace GestorDocumental.Data.Entities
{
    public class Usuario
    {
        [Key]
        public int CodigoUsuario { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellido1 { get; set; }

        public string? Apellido2 { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string Contraseña { get; set; }

        public string? Curso { get; set; }

        public string? Grupo { get; set; }

        public int CodigoRol {  get; set; }

        public string Asignatura { get; set; }

    }
}
