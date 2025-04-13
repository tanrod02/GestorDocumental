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

            if (context.Usuarios.Any(u => u.Correo == usuario.Correo))
                return false; // Ya existe un usuario con este correo

            //de base todos los que se registran son alumnos Rol = 3
            usuario.CodigoRol = 2;

            context.Usuarios.Add(usuario);
            context.SaveChanges();
            return true;
        }

        public async Task<Usuario?> ObtenerUsuarioPorCorreoAsync(string correo)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);
        }

        public async Task<List<Usuario>> ObtenerUsuariosPorGrupo(int codigoCurso, string Grupo)
        {
            using var context = _contextFactory.CreateDbContext();

            List<int> codigosUsuario = new List<int>();

            codigosUsuario = context.CursosUsuario.Where(a =>a.CodigoCurso == codigoCurso).Select(x => x.CodigoUsuario).ToList();

            List<Usuario> usuarios = new List<Usuario>();

            foreach(int codUsuario in codigosUsuario)
            {
                Usuario user = context.Usuarios.FirstOrDefault(a => a.CodigoUsuario == codUsuario);

                usuarios.Add(user);
            }

            usuarios = usuarios.Where(x => x.Grupo == Grupo).ToList();

            return usuarios;
        }
    }
}
