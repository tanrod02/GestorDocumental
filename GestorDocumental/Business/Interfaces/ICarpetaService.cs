using GestorDocumental.Data.Entities;

namespace GestorDocumental.Business.Interfaces
{
    public interface ICarpetaService
    {
        Task<List<Archivo>> ObtenerArchivosCarpeta(int CodigoCarpeta);
        Task<int> CrearCarpeta(Carpeta carpeta);
        Task EliminarCarpeta(Carpeta Carpeta);
        Task<Carpeta> ObtenerCarpeta(int CodigoCarpeta);
        Task ModificarCarpeta(Carpeta Carpeta);
    }
}
