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

        public async Task<List<Curso>> ObtenerCursosUsuario(int CodigoUsuario)
        {
            using var context = _contextFactory.CreateDbContext();
            List<int> cursosUsuario = await context.CursosUsuario.Where(cu => cu.CodigoUsuario == CodigoUsuario).Select(cu => cu.CodigoCurso).ToListAsync(); 
            return await context.Cursos.Where(c => cursosUsuario.Contains(c.CodigoCurso)).ToListAsync(); ;
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

        public async Task AgregarRelacionCursoUsuario(int CodigoCurso, int CodigoUsuario)
        {
            using var context = _contextFactory.CreateDbContext();
            CursosUsuario cursoUsuario = new CursosUsuario
            {
                CodigoCurso = CodigoCurso,
                CodigoUsuario = CodigoUsuario
            };
            await context.CursosUsuario.AddAsync(cursoUsuario);
            await context.SaveChangesAsync();
        }

        public async Task<string?> ObtenerCursoPorNombre(string descripcion)
        {
            using var context = _contextFactory.CreateDbContext();

            if (context.Cursos.Any(x => x.Descripcion == descripcion))
            {
                return descripcion;
            }
            else return null;
        }

        public async Task<List<Curso>> ObtenerCursos()
        {
            using var context = _contextFactory.CreateDbContext();

            return context.Cursos.ToList();
        }

        public async Task<List<Curso>> ObtenerCursosPorGrupo(string grupo)
        {

            using var context = _contextFactory.CreateDbContext();

            List<Curso> cursos = new List<Curso>();

            var ultimoDeParametro = grupo.Substring(grupo.Length - 1, 1);

            List<Grupos> grupos = context.Grupos
                .Where(x =>
                    x.Grupo != null
                    && x.Grupo.Length > 0
                    && x.Grupo.Substring(x.Grupo.Length - 1, 1) == ultimoDeParametro
                )
                .ToList();

            foreach (Grupos group in grupos)
            {
                Curso curso = context.Cursos.FirstOrDefault( x => x.CodigoCurso == group.CodigoCurso);

                if(!cursos.Any(x => x.CodigoCurso == curso.CodigoCurso))
                {
                    cursos.Add(curso);
                }
            }

            return cursos;
        }

        public async Task<List<Usuario>> ObtenerUsuariosPorGrupo(string grupo)
        {
            using var context = _contextFactory.CreateDbContext();

            var ultimoDeParametro = grupo.Substring(grupo.Length - 1, 1);

            List<Usuario> usuarios = context.Usuarios
                .Where(x =>
                    x.Grupo != null
                    && x.Grupo.Length > 0
                    && x.Grupo.Substring(x.Grupo.Length - 1, 1) == ultimoDeParametro
                )
                .ToList();

            return usuarios;
        }



    }
}
