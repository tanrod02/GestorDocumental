using GestorDocumental.Business.Interfaces;
using GestorDocumental.Data.Entities;
using GestorDocumental.Data.Interfaces;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GestorDocumental.Business.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> RegistrarUsuarioAsync(Usuario usuario)
        {
            usuario.Contraseña = HashPassword(usuario.Contraseña); 
            return await _usuarioRepository.RegistrarUsuarioAsync(usuario);
        }

        public async Task<Usuario?> IniciarSesionAsync(string correo, string contraseña)
        {
            var usuario = await _usuarioRepository.ObtenerUsuarioPorCorreoAsync(correo);
            if (usuario == null || usuario.Contraseña != HashPassword(contraseña))
                return null; // Usuario no encontrado o credenciales incorrectas

            return usuario;
        }

        private static string HashPassword(string password)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
