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
            if (archivo == null || archivo.Contenido == null || archivo.Contenido.Length == 0)
            {
                throw new ArgumentException("El archivo no es válido.");
            }

            archivo.FechaAlta = DateTime.UtcNow; 
            await _archivoRepository.AgregarArchivoAsync(archivo);
        }
    }
}
