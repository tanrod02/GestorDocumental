using GestorDocumental.Data.Entities;

namespace GestorDocumental.Data.Interfaces
{
    public interface IArchivoRepository
    {
        Task<(IEnumerable<Carpeta> Carpetas, IEnumerable<Archivo> ArchivosSinCarpeta)> ObtenerArchivosYCarpetasPorCursoAsync(int codigoCurso);

        Task<(IEnumerable<Carpeta> Carpetas, IEnumerable<Archivo> ArchivosSinCarpeta)> ObtenerArchivosYCarpetasPorCursoYGrupoAsync(int codigoCurso, string grupo);

        Task AgregarArchivoAsync(Archivo archivo);
        Task<Carpeta> ObtenerInfoCarpeta(int CodigoCarpeta);
        Task ModificarArchivo(Archivo archivo);
        Task<Archivo> ObtenerInfoArchivo(int CodigoArchivo);
        Task EliminarArchivo(int CodigoArchivo);
        Task<EstadisticasArchivo> ObtenerEstadisticasArchivo(int CodigoArchivo);
        Task<string> ObtenerInfoPropietario(int CodigoArchivo);
        Task GuardarListaArchivoAsync(List<Archivo> archivos);
        Task<List<Archivo>> BuscarArchivos(string Palabra, int CodigoUsuario);
        Task<List<Archivo>> ObtenerArchivosExpirados();
        Task<List<Grupos>> ObtenerGruposPorCurso(int CodigoCurso);


    }
}
