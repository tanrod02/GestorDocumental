using GestorDocumental.Data.Entities;

namespace GestorDocumental.Data.Interfaces
{
    public interface ICursoRepository
    {
        Task<IEnumerable<Curso>> ObtenerCursosAsync();

        Task AgregarCursoAsync(Curso curso);

    }
}
