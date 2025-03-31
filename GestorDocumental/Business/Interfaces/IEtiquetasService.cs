using GestorDocumental.Data.Entities;

namespace GestorDocumental.Business.Interfaces
{
    public interface IEtiquetasService
    {
        Task<List<Etiqueta>> ObtenerEtiquetas();
        Task<List<Etiqueta>> ObtenerEtiquetasArchivo(int codigoArchivo);
        Task EliminarEtiquetasArchivo(int codigoArchivo, List<Etiqueta> etiquetas);
        Task<Etiqueta> CrearEtiqueta(Etiqueta Etiqueta);
        Task AgregarEtiquetasArchivo(int codigoArchivo, List<Etiqueta> etiquetas);
    }
}
