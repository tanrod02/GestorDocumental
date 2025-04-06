using GestorDocumental.Data.Entities;
using GestorDocumental.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace GestorDocumental.Data.Repositories
{
    public class CursoRepository: ICursoRepository
    {
        private readonly IDbContextFactory<GestorDocumentalDbContext> _contextFactory;

        public CursoRepository(IDbContextFactory<GestorDocumentalDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Curso>> ObtenerCursosAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Cursos.ToListAsync();
        }

        public async Task AgregarCursoAsync(Curso curso)
        {
            using var context = _contextFactory.CreateDbContext();
            await context.Cursos.AddAsync(curso);
            await context.SaveChangesAsync();
        }

        public async Task<Curso> ObtenerCursoPorCodigoAsync(int codigoCurso)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Cursos.FirstOrDefaultAsync(c => c.CodigoCurso == codigoCurso);
        }

    }
}
