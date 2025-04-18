using GestorDocumental.Data.Entities;
using GestorDocumental.Data.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace GestorDocumental.Data.Repositories
{
    public class ArchivoRepository: IArchivoRepository
    {
        private readonly IDbContextFactory<GestorDocumentalDbContext> _contextFactory;


        public ArchivoRepository(IDbContextFactory<GestorDocumentalDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public async Task<(IEnumerable<Carpeta> Carpetas, IEnumerable<Archivo> ArchivosSinCarpeta)> ObtenerArchivosYCarpetasPorCursoAsync(int codigoCurso)
        {
            using var context = _contextFactory.CreateDbContext();

            List<Carpeta> carpetas = await context.Carpeta.Where(c => c.Curso == codigoCurso).ToListAsync();

            List<Archivo> archivosSinCarpeta = await context.Archivos.Where(a => a.Curso == codigoCurso &&
                a.CodigoCarpeta == null).ToListAsync();

            foreach (Archivo archivo in archivosSinCarpeta)
            {
                var codigoEtiquetas = await context.ArchivosEtiquetas
                    .Where(ae => ae.CodigoArchivo == archivo.CodigoArchivo)
                    .Select(ae => ae.CodigoEtiqueta)
                    .ToListAsync();

                var etiquetas = await context.Etiqueta
                    .Where(e => codigoEtiquetas.Contains(e.CodigoEtiqueta))
                    .Select(e => e.DescripcionEtiqueta)
                    .ToListAsync();

                archivo.Etiquetas = etiquetas;
            }

            return (carpetas, archivosSinCarpeta);
        }

        public async Task<(IEnumerable<Carpeta> Carpetas, IEnumerable<Archivo> ArchivosSinCarpeta)> ObtenerArchivosYCarpetasPorCursoYGrupoAsync(int codigoCurso, string grupo)
        {
            using var context = _contextFactory.CreateDbContext();

            List<Carpeta> carpetas = await context.Carpeta.Where(c => c.Curso == codigoCurso && c.Grupo == grupo).ToListAsync();

            List<Archivo> archivosSinCarpeta = await context.Archivos.Where(a => a.Curso == codigoCurso &&
                a.CodigoCarpeta == null && a.Grupo == grupo).ToListAsync();

            foreach (Archivo archivo in archivosSinCarpeta)
            {
                var codigoEtiquetas = await context.ArchivosEtiquetas
                    .Where(ae => ae.CodigoArchivo == archivo.CodigoArchivo)
                    .Select(ae => ae.CodigoEtiqueta)
                    .ToListAsync();

                var etiquetas = await context.Etiqueta
                    .Where(e => codigoEtiquetas.Contains(e.CodigoEtiqueta))
                    .Select(e => e.DescripcionEtiqueta)
                    .ToListAsync();

                archivo.Etiquetas = etiquetas;
            }

            return (carpetas, archivosSinCarpeta);
        }



        public async Task AgregarArchivoAsync(Archivo archivo)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                context.Archivos.Add(archivo);

                await context.SaveChangesAsync();

                EstadisticasArchivo est = new EstadisticasArchivo();

                est.Propietario = archivo.Propietario;
                est.CodigoArchivo = archivo.CodigoArchivo;
                est.FechaAcceso = null;
                est.FechaSubida = archivo.FechaAlta;
                est.NumeroVisitas = 0;
                est.TiempoEnDocumento = 0;

                context.Estadistica.Add(est);

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar archivo: {ex.Message}");
                throw; 
            }
        }

        public async Task<Carpeta> ObtenerInfoCarpeta(int CodigoCarpeta)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                return await context.Carpeta.FindAsync(CodigoCarpeta);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener infor de la carpeta: {ex.Message}");
                throw;
            }
        }

        

        public async Task ModificarArchivo(Archivo archivo)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                context.Archivos.Update(archivo);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al modificar archivo: {ex.Message}");
                throw;
            }
        }


        public async Task<Archivo> ObtenerInfoArchivo(int CodigoArchivo)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                return await context.Archivos.FindAsync(CodigoArchivo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener infor del archivo: {ex.Message}");
                throw;
            }
        }

        public async Task EliminarArchivo(int CodigoArchivo)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                Archivo archivo = await context.Archivos.FindAsync(CodigoArchivo);

                context.Archivos.Remove(archivo);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar archivo: {ex.Message}");
                throw;
            }
        }


        public async Task<EstadisticasArchivo> ObtenerEstadisticasArchivo(int CodigoArchivo)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                return await context.Estadistica.FirstOrDefaultAsync(e => e.CodigoArchivo == CodigoArchivo);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener infor del archivo: {ex.Message}");
                throw;
            }
        }

        public async Task<string> ObtenerInfoPropietario(int CodigoArchivo)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                Archivo archivo = await context.Archivos.FindAsync(CodigoArchivo);
                Usuario propietario = await context.Usuarios.FirstOrDefaultAsync(u => u.CodigoUsuario == archivo.Propietario);

                var nombre = $"{propietario.Nombre} {propietario.Apellido1}";

                if (!string.IsNullOrEmpty(propietario.Apellido2))
                {
                    nombre += " " + propietario.Apellido2;
                }

                return nombre;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener infor del propietario: {ex.Message}");
                throw;
            }
        }

        public async Task GuardarListaArchivoAsync(List<Archivo> archivos)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                await context.AddRangeAsync(archivos);

                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener infor del archivo: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Archivo>> BuscarArchivos(string Palabra, int CodigoUsuario)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                
                return await context.Archivos.Where(a => a.NombreArchivo.Contains(Palabra) && a.Curso == CodigoUsuario).ToListAsync();

                 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener infor del archivo: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Archivo>> ObtenerArchivosExpirados()
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                return await context.Archivos.Where(a => a.FechaBaja.HasValue && a.FechaBaja.Value < DateTime.Now).ToListAsync();


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener archivos de baja: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Grupos>> ObtenerGruposPorCurso(int CodigoCurso)
        {

            try
            {
                using var context = _contextFactory.CreateDbContext();

                return await context.Grupos.Where(a => a.CodigoCurso == CodigoCurso).ToListAsync();


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener archivos de baja: {ex.Message}");
                throw;
            }

        }


    }
}
