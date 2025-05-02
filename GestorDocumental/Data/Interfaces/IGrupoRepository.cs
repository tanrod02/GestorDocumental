using GestorDocumental.Data.Entities;

namespace GestorDocumental.Data.Interfaces
{
    public interface IGrupoRepository
    {
        Task AgregarGrupo(Grupos grupo);

        Task<List<Grupos>> ObtenerGruposPorCurso(int codigoCurso);
        Task<List<string>> ObtenerGrupos();
    }
}
