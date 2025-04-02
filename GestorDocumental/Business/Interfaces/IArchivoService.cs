using GestorDocumental.Data.Entities;
using Microsoft.AspNetCore.Components.Forms;

namespace GestorDocumental.Business.Interfaces
{
    public interface IArchivoService
    {
        Task<(IEnumerable<Carpeta> Carpetas, IEnumerable<Archivo> ArchivosSinCarpeta)> ObtenerArchivosYCarpetasPorCursoAsync(int codigoCurso);
        Task GuardarArchivoAsync(Archivo archivo);
        Task<Carpeta> ObtenerInfoCarpeta(int CodigoCarpeta);
        Task ModificarArchivo(Archivo archivo);
        Task<Archivo> ObtenerInfoArchivo(int CodigoArchivo);
        Task EliminarArchivo(int CodigoArchivo);
        Task<EstadisticasArchivo> ObtenerEstadisticasArchivo(int CodigoArchivo);
        Task<string> ObtenerInfoPropietario(int CodigoArchivo);
        Task GuardarListaArchivoAsync(List<Archivo> Archivos);
        Task<List<Archivo>> BuscarArchivos(string Palabra, int CodigoUsuario);
    }

}
