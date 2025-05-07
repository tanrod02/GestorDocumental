using GestorDocumental.Business.Interfaces;
using GestorDocumental.Data.Entities;
using GestorDocumental.Data.Interfaces;

namespace GestorDocumental.Business.Services
{
    public class EstadisticaService : IEstadisticaService
    {
        private readonly IEstadisticaRepository _estadisticaRepository;

        public EstadisticaService(IEstadisticaRepository estadisticaRepository)
        {
             _estadisticaRepository= estadisticaRepository;
        }

        public async Task ActualizarEstadisticasArchivo(Archivo archivo)
        {
           EstadisticasArchivo est = _estadisticaRepository.ObtenerEstadisticasArchivo(archivo.CodigoArchivo).Result;

            est.FechaAcceso = DateTime.Now;
            est.NumeroVisitas += 1;

            _estadisticaRepository.ActualizarEstadisticasArchivo(est).Wait();

        }

        public async Task<List<EstadisticasArchivo>> ObtenerEstadisticas(List<Archivo> archivos)
        {

            List<EstadisticasArchivo> estadisticas = new List<EstadisticasArchivo>();

            foreach (Archivo archivo in archivos)
            {
                EstadisticasArchivo est = _estadisticaRepository.ObtenerEstadisticasArchivo(archivo.CodigoArchivo).Result;
                estadisticas.Add(est);
            }

            return estadisticas;
        }

        public async Task GuardarTiempoVisualizacion(int codigoArchivo, TimeSpan tiempo)
        {
            _estadisticaRepository.GuardarTiempoVisualizacion(codigoArchivo,tiempo).Wait();
        }
    }
}
