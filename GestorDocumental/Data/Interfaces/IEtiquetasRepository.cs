using GestorDocumental.Data.Entities;

namespace GestorDocumental.Data.Interfaces
{
    public interface IEtiquetasRepository
    {
        Task<List<Etiqueta>> ObtenerEtiquetas();
        Task<List<Etiqueta>> ObtenerEtiquetasArchivo(int codigoArchivo);
        Task EliminarEtiquetaArchivo(int codigoArchivo,List<Etiqueta> etiquetasEliminar);
        Task<Etiqueta> CrearEtiqueta(Etiqueta Etiqueta);
        Task AgregarEtiquetasArchivo(int codigoArchivo, List<Etiqueta> etiquetas);
    }
}
