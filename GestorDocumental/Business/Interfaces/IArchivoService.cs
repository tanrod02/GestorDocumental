using GestorDocumental.Data.Entities;

namespace GestorDocumental.Business.Interfaces
{
    public interface IArchivoService
    {
        Task<IEnumerable<Archivo>> ObtenerArchivosPorCursoAsync(int codigoCurso);
    }

}
