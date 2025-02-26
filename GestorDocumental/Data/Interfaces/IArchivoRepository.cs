using GestorDocumental.Data.Entities;

namespace GestorDocumental.Data.Interfaces
{
    public interface IArchivoRepository
    {
        Task<IEnumerable<Archivo>> ObtenerArchivosPorCursoAsync(int codigoCurso);
    }
}
