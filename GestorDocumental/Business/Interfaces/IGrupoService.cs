using GestorDocumental.Data.Entities;

namespace GestorDocumental.Business.Interfaces
{
    public interface IGrupoService
    {
        Task AgregarGrupo(Grupos grupo);

        Task<List<Grupos>> ObtenerGruposPorCurso(int codigoCurso);
    }
}
