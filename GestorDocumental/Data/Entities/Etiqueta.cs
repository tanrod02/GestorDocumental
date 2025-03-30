using System.ComponentModel.DataAnnotations;

namespace GestorDocumental.Data.Entities
{
    public class Etiqueta
    {
        [Key]
        public int CodigoEtiqueta { get; set; }
        public string DescripcionEtiqueta { get; set; }
    }
}
