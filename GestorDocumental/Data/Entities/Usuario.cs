using System.ComponentModel.DataAnnotations;

namespace GestorDocumental.Data.Entities
{
    public class Usuario
    {
        [Key]
        public int CodigoUsuario { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido1 { get; set; }
        public string? Apellido2 { get; set; }
        [Required]
        public string Correo { get; set; }
        [Required]
        public string Contraseña { get; set; }
        public string? Curso { get; set; }
        public string? Grupo { get; set; }
        public int CodigoRol {  get; set; }
        public string Asignatura { get; set; }

    }
}
