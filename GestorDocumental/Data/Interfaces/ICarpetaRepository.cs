using GestorDocumental.Data.Entities;

namespace GestorDocumental.Data.Interfaces
{
    public interface ICarpetaRepository
    {
        Task<IEnumerable<Archivo>> ObtenerArchivosCarpeta(int CodigoCarpeta);
        Task<int> CrearCarpeta(Carpeta carpeta);
        Task EliminarCarpeta(Carpeta Carpeta);
        Task<Carpeta> ObtenerCarpeta(int CodigoCarpeta);
        Task ModificarCarpeta(Carpeta Carpeta);
    }
}
