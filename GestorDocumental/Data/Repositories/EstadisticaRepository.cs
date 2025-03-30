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

            context.SaveChangesAsync();
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
                Console.WriteLine($"Error al obtener infor del archivo: {ex.Message}");
                throw;
            }
        }
    }
}
