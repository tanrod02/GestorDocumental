using GestorDocumental.Business.Interfaces;
using GestorDocumental.Data.Entities;
using GestorDocumental.Data.Interfaces;

namespace GestorDocumental.Business.Services
{
    public class ArchivoService: IArchivoService
    {
        private readonly IArchivoRepository _archivoRepository;

        public ArchivoService(IArchivoRepository archivoRepository)
        {
            _archivoRepository = archivoRepository;
        }

        public async Task<IEnumerable<Archivo>> ObtenerArchivosPorCursoAsync(int codigoCurso)
        {
            return await _archivoRepository.ObtenerArchivosPorCursoAsync(codigoCurso);
        }

    }
}
