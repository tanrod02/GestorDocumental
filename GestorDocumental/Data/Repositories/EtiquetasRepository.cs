using GestorDocumental.Data.Entities;
using GestorDocumental.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestorDocumental.Data.Repositories
{
    public class EtiquetasRepository: IEtiquetasRepository
    {
        private readonly IDbContextFactory<GestorDocumentalDbContext> _contextFactory;


        public EtiquetasRepository(IDbContextFactory<GestorDocumentalDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Etiqueta>> ObtenerEtiquetas()
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                return await context.Etiqueta.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las etiquetas {ex.Message}");
                throw;
            }
        }

        public async Task<List<Etiqueta>> ObtenerEtiquetasArchivo(int codigoArchivo)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                List<Etiqueta> etiquetas = await context.ArchivosEtiquetas
                                              .Where(ea => ea.CodigoArchivo == codigoArchivo) 
                                              .Join(context.Etiqueta, 
                                                    ea => ea.CodigoEtiqueta, 
                                                    e => e.CodigoEtiqueta,
                                                    (ea, e) => e) 
                                              .ToListAsync(); 

                return etiquetas;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las etiquetas de un archivo: {ex.Message}");
                throw;
            }
        }

        public async Task EliminarEtiquetaArchivo(int codigoArchivo,List<Etiqueta> etiquetasEliminar)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var relaciones = await context.ArchivosEtiquetas.Where(ae => etiquetasEliminar.Select(e => e.CodigoEtiqueta)
                .Contains(ae.CodigoEtiqueta) && ae.CodigoArchivo == codigoArchivo).ToListAsync();

                context.ArchivosEtiquetas.RemoveRange(relaciones); 

                await context.SaveChangesAsync(); 

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las etiquetas {ex.Message}");
                throw;
            }
        }

        public async Task<Etiqueta> CrearEtiqueta(Etiqueta Etiqueta)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                context.Etiqueta.Add(Etiqueta);

                await context.SaveChangesAsync();

                return Etiqueta;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear etiqueta {ex.Message}");
                throw;
            }
        }

        public async Task AgregarEtiquetasArchivo(int codigoArchivo, List<Etiqueta> etiquetas)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var relaciones = etiquetas.Select(etiqueta => new ArchivoEtiqueta
                {
                    CodigoArchivo = codigoArchivo,
                    CodigoEtiqueta = etiqueta.CodigoEtiqueta
                }).ToList();

                context.ArchivosEtiquetas.AddRange(relaciones);

                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar las etiquetas al archivo {ex.Message}");
                throw;
            }
        }


        public async Task<List<ArchivoEtiqueta>> ObtenerRelacionesEtiquetasArchivos(int CodigoArchivo)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                return await context.ArchivosEtiquetas.Where(ae => ae.CodigoArchivo == CodigoArchivo).ToListAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar las etiquetas al archivo {ex.Message}");
                throw;
            }
        }
    }
}
