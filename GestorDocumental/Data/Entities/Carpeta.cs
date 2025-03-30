using System.ComponentModel.DataAnnotations;

namespace GestorDocumental.Data.Entities
{
    public class Carpeta
    {
        [Key]
        public int CodigoCarpeta { get; set; }
        public string Descripcion { get; set; }
        public string Path { get; set; }
        public int? Propietario { get; set; }
        public int? Curso { get; set; }
        public string? Grupo { get; set; }
        public DateTime Subida { get; set; }
    }
}
