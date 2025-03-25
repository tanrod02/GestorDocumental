using System.ComponentModel.DataAnnotations;

namespace GestorDocumental.Data.Entities
{
    public class EstadisticasArchivo
    {
        [Key]
        public int CodigoEstadistica { get; set; }
        public int CodigoArchivo { get; set; }
        public int? NumeroVisitas { get; set; }
        public int TiempoEnDocumento { get; set; }
        public DateTime FechaSubida { get; set; }
        public DateTime? FechaAcceso { get; set; }
        public int Propietario { get; set; }
  
    }
}
