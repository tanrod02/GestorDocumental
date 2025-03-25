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

            var carpetas = await context.Carpeta.Where(c => c.Curso == codigoCurso).ToListAsync();

            var archivosSinCarpeta = await context.Archivos.Where(a => a.Curso == codigoCurso).ToListAsync();

            return (carpetas, archivosSinCarpeta);
        }



        public async Task AgregarArchivoAsync(Archivo archivo)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                context.Archivos.Add(archivo);
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

        public async Task<IEnumerable<Archivo>> ObtenerArchivosCarpeta(int CodigoCarpeta)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                return await context.Archivos.Where(a => a.CodigoCarpeta == CodigoCarpeta).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar los archivos de la carpeta: {ex.Message}");
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

    }
}
