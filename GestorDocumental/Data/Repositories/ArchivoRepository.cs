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


        public async Task<IEnumerable<Archivo>> ObtenerArchivosPorCursoAsync(int codigoCurso)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Archivos.Where(a => a.Curso.HasValue && a.Curso == codigoCurso) 
      .ToListAsync();

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


        public async Task VerificarArchivoGuardadoEnDB(string nombreArchivo)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                // Buscar el archivo en la base de datos usando LINQ
                var archivo = await context.Archivos
                    .Where(a => a.NombreArchivo == nombreArchivo)
                    .FirstOrDefaultAsync();

                if (archivo != null && archivo.Contenido != null && archivo.Contenido.Length > 0)
                {
                    Console.WriteLine($"Archivo {archivo.NombreArchivo} recuperado correctamente desde la base de datos.");

                    // Guardar el archivo en una ubicación temporal para verificar
                    var filePath = Path.Combine(Path.GetTempPath(), archivo.NombreArchivo);
                    File.WriteAllBytes(filePath, archivo.Contenido);

                    Console.WriteLine($"Archivo guardado en la ruta temporal: {filePath}");
                }
                else
                {
                    Console.WriteLine($"El archivo {nombreArchivo} está vacío o no se guardó correctamente.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al verificar archivo: {ex.Message}");
            }
        }



    }
}
