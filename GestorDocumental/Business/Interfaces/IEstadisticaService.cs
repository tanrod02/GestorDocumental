using GestorDocumental.Data.Entities;

namespace GestorDocumental.Business.Interfaces
{
    public interface IEstadisticaService
    {
        Task ActualizarEstadisticasArchivo(Archivo archivo);
        Task<List<EstadisticasArchivo>> ObtenerEstadisticas(List<Archivo> archivos);
    }
}
