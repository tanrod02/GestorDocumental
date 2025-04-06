using GestorDocumental.Data.Entities;
using GestorDocumental.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestorDocumental.Data.Repositories
{
    public class CarpetaRepository: ICarpetaRepository
    {
        private readonly IDbContextFactory<GestorDocumentalDbContext> _contextFactory;


        public CarpetaRepository(IDbContextFactory<GestorDocumentalDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<List<Archivo>> ObtenerArchivosCarpeta(int CodigoCarpeta)
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

        public async Task<int> CrearCarpeta(Carpeta carpeta)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                if (carpeta.Propietario == null || !context.Usuarios.Any(u => u.CodigoUsuario == carpeta.Propietario))
                {
                    throw new Exception("El usuario especificado no existe.");
                }
                context.Carpeta.Add(carpeta);

                await context.SaveChangesAsync();

                return carpeta.CodigoCarpeta;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear una carpeta: {ex.Message}");
                throw;
            }
        }

        public async Task EliminarCarpeta(Carpeta Carpeta)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                context.Carpeta.Remove(Carpeta);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar carpeta: {ex.Message}");
                throw;
            }
        }

        public async Task<Carpeta> ObtenerCarpeta(int CodigoCarpeta)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                return await context.Carpeta.FindAsync(CodigoCarpeta);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener carpeta: {ex.Message}");
                throw;
            }
        }

        public async Task ModificarCarpeta(Carpeta Carpeta)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                context.Carpeta.Update(Carpeta);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al modificar carpeta: {ex.Message}");
                throw;
            }
        }
    }
}
