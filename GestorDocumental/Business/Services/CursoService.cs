using GestorDocumental.Business.Interfaces;
using GestorDocumental.Data.Entities;
using GestorDocumental.Data.Interfaces;

namespace GestorDocumental.Business.Services
{
    public class CursoService : ICursoService
    {
        private readonly ICursoRepository _cursoRepository;

        public CursoService(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        public async Task<List<Curso>> ObtenerCursosUsuario(int CodigoUsuario)
        {
            return await _cursoRepository.ObtenerCursosUsuario(CodigoUsuario);
        }

        public async Task AgregarCursoAsync(Curso curso)
        {
            await _cursoRepository.AgregarCursoAsync(curso);
        }

        public async Task<Curso> ObtenerCursoPorCodigoAsync(int codigoCurso)
        {
            if (codigoCurso <= 0)
                throw new ArgumentException("El código del curso no es válido.", nameof(codigoCurso));

            return await _cursoRepository.ObtenerCursoPorCodigoAsync(codigoCurso);
        }

        public async Task AgregarRelacionCursoUsuario(int CodigoCurso, int CodigoUsuario)
        {
            await _cursoRepository.AgregarRelacionCursoUsuario(CodigoCurso, CodigoUsuario);
        }

        public async Task<string?> ObtenerCursoPorNombre(string descripcion)
        {
            return await _cursoRepository.ObtenerCursoPorNombre(descripcion);
        }

        public async Task<List<Curso>> ObtenerCursos()
        {
            return await _cursoRepository.ObtenerCursos();
        }
    }
}
