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
        public DbSet<EstadisticasArchivo> Estadistica { get; set; }
        public DbSet<Etiqueta> Etiqueta { get; set; }
        public DbSet<ArchivoEtiqueta> ArchivosEtiquetas{get; set;}
        public DbSet<CursosUsuario> CursosUsuario{get; set; }
        public DbSet<Grupos> Grupos { get; set; }
        public DbSet<UserLog> UserLogs { get; set; }
        public DbSet<UserLogAudit> UserLogAudits { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Archivo>().HasKey(d => d.CodigoArchivo);
            modelBuilder.Entity<Curso>().HasKey(d => d.CodigoCurso);
            modelBuilder.Entity<ArchivoEtiqueta>().HasKey(ae => new { ae.CodigoEtiqueta, ae.CodigoArchivo });
            modelBuilder.Entity<CursosUsuario>().HasKey(cu => new {cu.CodigoCurso, cu.CodigoUsuario });
        }


    }
}
