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
            return await context.Archivos.Where(a => a.Curso.HasValue && a.Curso == codigoCurso) // Filtra valores NULL
      .ToListAsync();

        }


    }
}
