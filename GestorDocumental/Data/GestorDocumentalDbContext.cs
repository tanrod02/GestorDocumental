using GestorDocumental.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestorDocumental.Data
{
    public class GestorDocumentalDbContext : DbContext
    {
        public GestorDocumentalDbContext(DbContextOptions<GestorDocumentalDbContext> options) : base(options) { }

        // DbSet para cada entidad
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Archivo> Archivos { get; set; }
        public DbSet<Carpeta> Carpeta { get; set; }
        public DbSet<Curso> Cursos { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Archivo>().HasKey(d => d.CodigoArchivo);
            modelBuilder.Entity<Curso>().HasKey(d => d.CodigoCurso);
        }


    }
}
