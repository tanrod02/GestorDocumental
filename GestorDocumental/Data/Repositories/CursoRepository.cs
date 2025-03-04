using GestorDocumental.Data.Entities;
using GestorDocumental.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestorDocumental.Data.Repositories
{
    public class CursoRepository: ICursoRepository
    {
        private readonly GestorDocumentalDbContext _context;

        public CursoRepository(GestorDocumentalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Curso>> ObtenerCursosAsync()
        {
            return await _context.Cursos.ToListAsync();
        }

        public async Task AgregarCursoAsync(Curso curso)
        {
            await _context.Cursos.AddAsync(curso);
            await _context.SaveChangesAsync();
        }

    }
}
