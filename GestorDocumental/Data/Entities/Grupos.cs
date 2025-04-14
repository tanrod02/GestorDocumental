using System.ComponentModel.DataAnnotations;

namespace GestorDocumental.Data.Entities
{
    public class Grupos
    {
        [Key]
        public string Grupo { get; set; }

        public int CodigoCurso { get; set; }

    }
}
