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

            // Obtener las carpetas asociadas al curso
            var carpetas = await context.Carpeta
                                        .Where(c => c.Curso == codigoCurso)
                                        .ToListAsync();

            // Obtener los archivos sin carpeta asociada
            var archivosSinCarpeta = await context.Archivos
                                                  .Where(a => a.Curso == codigoCurso)
                                                  .ToListAsync();

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



    }
}
