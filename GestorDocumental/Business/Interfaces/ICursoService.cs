using GestorDocumental.Data.Entities;

namespace GestorDocumental.Business.Interfaces
{
    public interface ICursoService
    {
        Task<IEnumerable<Curso>> ObtenerCursosAsync();

        Task AgregarCursoAsync(Curso curso);
    }
}
