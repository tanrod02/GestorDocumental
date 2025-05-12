using System.ComponentModel.DataAnnotations;

namespace GestorDocumental.Data.Entities
{
    public class Grupos
    {
        [Key]
        [Required(ErrorMessage = "El nombre del grupo es obligatorio")]
        public string Grupo { get; set; }

        public int CodigoCurso { get; set; }

    }
}
