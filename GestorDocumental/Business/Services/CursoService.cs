using GestorDocumental.Business.Interfaces;
using GestorDocumental.Data.Entities;
using GestorDocumental.Data.Interfaces;

namespace GestorDocumental.Business.Services
{
    public class CursoService: ICursoService
    {
        private readonly ICursoRepository _cursoRepository;

        public CursoService(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        public async Task<IEnumerable<Curso>> ObtenerCursosAsync()
        {
            return await _cursoRepository.ObtenerCursosAsync();
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
    }
}
