using GestorDocumental.Data.Entities;

namespace GestorDocumental.Data.Interfaces
{
    public interface ICursoRepository
    {
        Task<List<Curso>> ObtenerCursosUsuario(int CodigoUsuario);

        Task AgregarCursoAsync(Curso curso);
        Task<Curso> ObtenerCursoPorCodigoAsync(int codigoCurso);
        Task AgregarRelacionCursoUsuario(int CodigoCurso, int CodigoUsuario);

        Task<string?> ObtenerCursoPorNombre(string descripcion);

        Task<List<Curso>> ObtenerCursos();

        Task<List<Curso>> ObtenerCursosPorGrupo(string grupo);

        Task<List<Usuario>> ObtenerUsuariosPorGrupo(string grupo);


    }
}
