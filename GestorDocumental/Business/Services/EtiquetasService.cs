using System.Runtime.CompilerServices;
using GestorDocumental.Business.Interfaces;
using GestorDocumental.Data.Entities;
using GestorDocumental.Data.Interfaces;

namespace GestorDocumental.Business.Services
{
    public class EtiquetasService: IEtiquetasService
    {
        private readonly IEtiquetasRepository _etiquetasRepository;
        public EtiquetasService(IEtiquetasRepository etiquetasRepository)
        {
            _etiquetasRepository = etiquetasRepository;
        }
        public async Task<List<Etiqueta>> ObtenerEtiquetas()
        {
            return await _etiquetasRepository.ObtenerEtiquetas();
        }

        public async Task<List<Etiqueta>> ObtenerEtiquetasArchivo(int codigoArchivo)
        {
            return await _etiquetasRepository.ObtenerEtiquetasArchivo(codigoArchivo);
        }

        public async Task EliminarEtiquetasArchivo(int codigoArchivo, List<Etiqueta> etiquetasEliminar)
        {
            await _etiquetasRepository.EliminarEtiquetaArchivo(codigoArchivo, etiquetasEliminar);
        }

        public async Task<Etiqueta> CrearEtiqueta(Etiqueta Etiqueta)
        {
            return await _etiquetasRepository.CrearEtiqueta(Etiqueta);
        }

        public async Task AgregarEtiquetasArchivo(int codigoArchivo, List<Etiqueta> etiquetas)
        {
            await _etiquetasRepository.AgregarEtiquetasArchivo(codigoArchivo , etiquetas);
        }

        public async Task EliminarTodasEtiquetasArchivo(int CodigoArchivo)
        {
            await _etiquetasRepository.EliminarTodasEtiquetasArchivo(CodigoArchivo);
        }
    }
}
