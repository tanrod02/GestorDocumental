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

        public async Task<(IEnumerable<Carpeta> Carpetas, IEnumerable<Archivo> ArchivosSinCarpeta)> ObtenerArchivosYCarpetasPorCursoAsync(int codigoCurso)
        {
            return await _archivoRepository.ObtenerArchivosYCarpetasPorCursoAsync(codigoCurso);
        }


        public async Task GuardarArchivoAsync(Archivo archivo)
        {
            await _archivoRepository.AgregarArchivoAsync(archivo);
        }

        public async Task<Carpeta> ObtenerInfoCarpeta(int CodigoCarpeta)
        {
            return await _archivoRepository.ObtenerInfoCarpeta(CodigoCarpeta);
        }

       
        public async Task ModificarArchivo(Archivo archivo)
        {
            await _archivoRepository.ModificarArchivo(archivo);
        }

        public async Task<Archivo> ObtenerInfoArchivo(int CodigoArchivo)
        {
            return await _archivoRepository.ObtenerInfoArchivo(CodigoArchivo);
        }

        public async Task EliminarArchivo(int CodigoArchivo)
        {
            await _archivoRepository.EliminarArchivo(CodigoArchivo);
        }

        public async Task<EstadisticasArchivo> ObtenerEstadisticasArchivo(int CodigoArchivo)
        {
            return await _archivoRepository.ObtenerEstadisticasArchivo(CodigoArchivo);
        }

        public async Task<string> ObtenerInfoPropietario(int CodigoArchivo)
        {
            return await _archivoRepository.ObtenerInfoPropietario(CodigoArchivo);
        }

        public async Task GuardarListaArchivoAsync(List<Archivo> archivos)
        {
            await _archivoRepository.GuardarListaArchivoAsync(archivos);
        }

        public async Task<List<Archivo>> BuscarArchivos(string Palabra, int CodigoUsuario)
        {
            return await _archivoRepository.BuscarArchivos( Palabra, CodigoUsuario);
        }
    }
}
