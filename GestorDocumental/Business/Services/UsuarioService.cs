using GestorDocumental.Business.Interfaces;
using GestorDocumental.Data.Entities;
using GestorDocumental.Data.Interfaces;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;
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
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(contraseña, usuario.Contraseña))
            {
                return null; // Usuario no encontrado o credenciales incorrectas
            }

            return usuario;
        }


        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }


    }
}
