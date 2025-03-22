using GestorDocumental.Data.Entities;
using Microsoft.AspNetCore.Components.Forms;

namespace GestorDocumental.Business.Interfaces
{
    public interface IArchivoService
    {
        Task<IEnumerable<Archivo>> ObtenerArchivosPorCursoAsync(int codigoCurso);
        Task GuardarArchivoAsync(Archivo archivo);
        Task VerificarArchivoGuardadoEnDB(string nombreArchivo);
    }

}
