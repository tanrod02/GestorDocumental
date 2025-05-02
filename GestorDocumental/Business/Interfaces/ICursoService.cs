using GestorDocumental.Data.Entities;

namespace GestorDocumental.Business.Interfaces
{
    public interface ICursoService
    {
        Task<List<Curso>> ObtenerCursosUsuario(int CodigoUsuario);

        Task AgregarCursoAsync(Curso curso);
        Task<Curso> ObtenerCursoPorCodigoAsync(int codigoCurso);
        Task AgregarRelacionCursoUsuario(int CodigoCurso, int CodigoUsuario, string grupo);

        Task AgregarRelacionCursosUsuario(int CodigoCurso, int CodigoUsuario, string Grupo);

        Task AgregarRelacionCursosUsuarioGrupo(int codigoCurso, string grupo);

        Task<string?> ObtenerCursoPorNombre(string descripcion);

        Task<List<Curso>> ObtenerCursos();

        Task AgregarRelacionUsuarioGrupo(int CodigoUsuario, string grupo);

    }
}
