using GestorDocumental.Data.Entities;
using GestorDocumental.Data.Interfaces;
using System.Threading;

namespace GestorDocumental.Business.Services
{
    public class RetencionDocumentalService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly Timer _timer;

        public RetencionDocumentalService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;

            // Ejecuta inmediatamente y luego cada 24 horas
            _timer = new Timer(
                async state => await EjecutarRetencionAsync(),
                null,
                TimeSpan.Zero,
                TimeSpan.FromHours(24)
            );
        }

        private async Task EjecutarRetencionAsync()
        {
            using var scope = _scopeFactory.CreateScope();

            var archivoRepo = scope.ServiceProvider.GetRequiredService<IArchivoRepository>();
            var etiquetasRepo = scope.ServiceProvider.GetRequiredService<IEtiquetasRepository>();
            var estadisticasRepo = scope.ServiceProvider.GetRequiredService<IEstadisticaRepository>();

            var archivosAEliminar = await archivoRepo.ObtenerArchivosExpirados();

            foreach (var archivo in archivosAEliminar)
            {
                // Eliminar etiquetas
                var etiquetas = await etiquetasRepo.ObtenerEtiquetasArchivo(archivo.CodigoArchivo);
                await etiquetasRepo.EliminarEtiquetaArchivo(archivo.CodigoArchivo, etiquetas);

                // Eliminar estadísticas
                var est = await estadisticasRepo.ObtenerEstadisticasArchivo(archivo.CodigoArchivo);
                if (est != null)
                    await estadisticasRepo.EliminarEstadisticasArchivo(est);

                // Eliminar el archivo
                await archivoRepo.EliminarArchivo(archivo.CodigoArchivo);
            }
        }
    }
}
