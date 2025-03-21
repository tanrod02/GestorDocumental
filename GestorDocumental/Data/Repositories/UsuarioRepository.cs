using GestorDocumental.Data.Entities;
using GestorDocumental.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GestorDocumental.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbContextFactory<GestorDocumentalDbContext> _contextFactory;

        public UsuarioRepository(IDbContextFactory<GestorDocumentalDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<bool> RegistrarUsuarioAsync(Usuario usuario)
        {
            using var context = _contextFactory.CreateDbContext();

            if (await context.Usuarios.AnyAsync(u => u.Correo == usuario.Correo))
                return false; // Ya existe un usuario con este correo

            //de base todos los que se registran son alumnos Rol = 3
            usuario.CodigoRol = 3;

            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Usuario?> ObtenerUsuarioPorCorreoAsync(string correo)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);
        }
    }
}
