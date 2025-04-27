using GestorDocumental.Data.Entities;

namespace GestorDocumental.Business.Interfaces
{
    public interface ICursoService
    {
        Task<List<Curso>> ObtenerCursosUsuario(int CodigoUsuario);

        Task AgregarCursoAsync(Curso curso);
        Task<Curso> ObtenerCursoPorCodigoAsync(int codigoCurso);
        Task AgregarRelacionCursoUsuario(int CodigoCurso, int CodigoUsuario);

        Task<string?> ObtenerCursoPorNombre(string descripcion);

        Task<List<Curso>> ObtenerCursos();

    }
}
