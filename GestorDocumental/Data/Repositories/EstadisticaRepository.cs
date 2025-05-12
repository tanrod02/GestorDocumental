using GestorDocumental.Data.Entities;
using GestorDocumental.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestorDocumental.Data.Repositories
{
    public class EstadisticaRepository : IEstadisticaRepository
    {

        private readonly IDbContextFactory<GestorDocumentalDbContext> _contextFactory;


        public EstadisticaRepository(IDbContextFactory<GestorDocumentalDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task ActualizarEstadisticasArchivo(EstadisticasArchivo estadisticas)
        {
            using var context = _contextFactory.CreateDbContext();

            context.Update(estadisticas);

            await context.SaveChangesAsync();
        }

        public async Task<EstadisticasArchivo> ObtenerEstadisticasArchivo(int CodigoArchivo)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                return context.Estadistica.FirstOrDefault(e => e.CodigoArchivo == CodigoArchivo);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener estadisticas del archivo: {ex.Message}");
                throw;
            }
        }

        public async Task EliminarEstadisticasArchivo(EstadisticasArchivo Estadisticas)
        {
            using var context = _contextFactory.CreateDbContext();

            context.Remove(Estadisticas);

            await context.SaveChangesAsync();
        }

        public async Task GuardarListaEstadisticasAsync(List<EstadisticasArchivo> Estadisticas)
        {
            using var context = _contextFactory.CreateDbContext();

            context.AddRange(Estadisticas);

            await context.SaveChangesAsync();
        }


        public async Task GuardarTiempoVisualizacion(int codigoArchivo, TimeSpan tiempo)
        {
            using var context = _contextFactory.CreateDbContext();

            EstadisticasArchivo? estadistica = await context.Estadistica.FirstOrDefaultAsync(x => x.CodigoArchivo == codigoArchivo);

            if (estadistica != null)
            {
                estadistica.TiempoEnDocumento += tiempo.Seconds;
                context.Update(estadistica);
            }

            await context.SaveChangesAsync();
        
        }


    }
}
