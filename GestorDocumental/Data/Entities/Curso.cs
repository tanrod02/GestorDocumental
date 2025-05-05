using System.ComponentModel.DataAnnotations;

namespace GestorDocumental.Data.Entities
{
    public class Curso
    {
        [Key]
        public int CodigoCurso { get; set; }
        [Required(ErrorMessage = "El nombre del curso es obligatorio")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Indicar el grupo")]
        public string Grupo { get; set; }
    }
}
