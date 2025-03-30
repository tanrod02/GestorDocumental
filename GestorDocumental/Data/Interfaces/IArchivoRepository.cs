using GestorDocumental.Data.Entities;

namespace GestorDocumental.Data.Interfaces
{
    public interface IArchivoRepository
    {
        Task<(IEnumerable<Carpeta> Carpetas, IEnumerable<Archivo> ArchivosSinCarpeta)> ObtenerArchivosYCarpetasPorCursoAsync(int codigoCurso);
        Task AgregarArchivoAsync(Archivo archivo);
        Task<Carpeta> ObtenerInfoCarpeta(int CodigoCarpeta);
        Task ModificarArchivo(Archivo archivo);
        Task<Archivo> ObtenerInfoArchivo(int CodigoArchivo);
        Task EliminarArchivo(int CodigoArchivo);
        Task<EstadisticasArchivo> ObtenerEstadisticasArchivo(int CodigoArchivo);
        Task<string> ObtenerInfoPropietario(int CodigoArchivo);
        Task GuardarListaArchivoAsync(List<Archivo> archivos);
    }
}
