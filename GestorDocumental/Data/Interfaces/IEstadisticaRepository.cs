using GestorDocumental.Data.Entities;

namespace GestorDocumental.Data.Interfaces
{
    public interface IEstadisticaRepository
    {
        Task ActualizarEstadisticasArchivo(EstadisticasArchivo estadisticas);

        Task<EstadisticasArchivo> ObtenerEstadisticasArchivo(int CodigoArchivo);
    }
}
