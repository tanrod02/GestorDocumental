using GestorDocumental.Data.Entities;

namespace GestorDocumental.Business.Interfaces
{
    public interface ICarpetaService
    {
        Task<IEnumerable<Archivo>> ObtenerArchivosCarpeta(int CodigoCarpeta);
        Task<int> CrearCarpeta(Carpeta carpeta);
    }
}
