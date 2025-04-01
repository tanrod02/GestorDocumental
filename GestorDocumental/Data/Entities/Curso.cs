using System.ComponentModel.DataAnnotations;

namespace GestorDocumental.Data.Entities
{
    public class Curso
    {
        [Key]
        public int CodigoCurso { get; set; }
        public string Descripcion { get; set; }
        public string Grupo { get; set; }
    }
}
