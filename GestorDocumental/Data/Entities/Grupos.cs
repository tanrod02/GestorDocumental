using System.ComponentModel.DataAnnotations;

namespace GestorDocumental.Data.Entities
{
    public class Grupos
    {
        [Key]
        public int CodigoCurso { get; set; }

        public string Grupo { get; set; }

    }
}
