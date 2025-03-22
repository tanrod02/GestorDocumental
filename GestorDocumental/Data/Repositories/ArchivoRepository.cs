using GestorDocumental.Data.Entities;
using GestorDocumental.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestorDocumental.Data.Repositories
{
    public class ArchivoRepository: IArchivoRepository
    {
        private readonly IDbContextFactory<GestorDocumentalDbContext> _contextFactory;


        public ArchivoRepository(IDbContextFactory<GestorDocumentalDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public async Task<IEnumerable<Archivo>> ObtenerArchivosPorCursoAsync(int codigoCurso)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Archivos.Where(a => a.Curso.HasValue && a.Curso == codigoCurso) 
      .ToListAsync();

        }


        public async Task AgregarArchivoAsync(Archivo archivo)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                context.Archivos.Add(archivo);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar archivo: {ex.Message}");
                throw; 
            }
        }



    }
}
