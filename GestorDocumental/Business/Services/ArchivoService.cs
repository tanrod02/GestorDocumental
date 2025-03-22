using GestorDocumental.Business.Interfaces;
using GestorDocumental.Data.Entities;
using GestorDocumental.Data.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GestorDocumental.Business.Services
{
    public class ArchivoService : IArchivoService
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

        public async Task GuardarArchivoAsync(Archivo archivo)
        {
            await _archivoRepository.AgregarArchivoAsync(archivo);
        }

        public async Task VerificarArchivoGuardadoEnDB(string archivo)
        {
            await _archivoRepository.VerificarArchivoGuardadoEnDB(archivo);
        }
    }
}
