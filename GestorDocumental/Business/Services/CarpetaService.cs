using GestorDocumental.Business.Interfaces;
using GestorDocumental.Data.Entities;
using GestorDocumental.Data.Interfaces;
using GestorDocumental.Data.Repositories;

namespace GestorDocumental.Business.Services
{
    public class CarpetaService: ICarpetaService
    {
        private readonly ICarpetaRepository _carpetaRepository;
        public CarpetaService(ICarpetaRepository carpetaRepository)
        {
            _carpetaRepository = carpetaRepository;
        }

        public async Task<IEnumerable<Archivo>> ObtenerArchivosCarpeta(int CodigoCarpeta)
        {
            return await _carpetaRepository.ObtenerArchivosCarpeta(CodigoCarpeta);
        }

        public async Task<int> CrearCarpeta(Carpeta carpeta)
        {
            return await _carpetaRepository.CrearCarpeta(carpeta);
        }

    }
}
