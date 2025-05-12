using GestorDocumental.Data.Entities;

namespace GestorDocumental.Data.Interfaces
{
    public interface IEstadisticaRepository
    {
        Task ActualizarEstadisticasArchivo(EstadisticasArchivo estadisticas);

        Task<EstadisticasArchivo> ObtenerEstadisticasArchivo(int CodigoArchivo);
        Task EliminarEstadisticasArchivo(EstadisticasArchivo Estadisticas);
        Task GuardarListaEstadisticasAsync(List<EstadisticasArchivo> Estadisticas);
        Task GuardarTiempoVisualizacion(int codigoArchivo, TimeSpan tiempo);
    }
}
