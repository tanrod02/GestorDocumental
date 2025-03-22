using System.ComponentModel.DataAnnotations;

namespace GestorDocumental.Data.Entities
{
    public class Archivo
    {
        [Key]
        public int CodigoArchivo { get; set; }
        public int? CodigoCarpeta { get; set; }
        public string NombreArchivo { get; set; }
        public string Tipo { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
        public byte[] Contenido { get; set; }
        public int Propietario { get; set; }
        public int? Curso { get; set; }
        public string? Grupo { get; set; }
        public string? Etiquetas { get; set; }

        public int? Tamaño { get; set; }
        public bool Visible { get; set; }
    }
}
