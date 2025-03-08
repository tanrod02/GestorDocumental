using GestorDocumental.Business.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace GestorDocumental.Business.Services
{
    public class AuthService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly NavigationManager _navigationManager;

        // Estado de la autenticación en memoria
        public bool IsAuthenticated { get; private set; }

        public AuthService(IUsuarioService usuarioService, NavigationManager navigationManager)
        {
            _usuarioService = usuarioService;
            _navigationManager = navigationManager;
            IsAuthenticated = false; // El usuario no está autenticado al inicio
        }

        // Método de login
        public async Task<bool> LoginAsync(string correo, string contraseña)
        {
            var usuario = await _usuarioService.IniciarSesionAsync(correo, contraseña);
            if (usuario != null)
            {
                IsAuthenticated = true;
                // Después de iniciar sesión con éxito, redirigir a la página de inicio
                _navigationManager.NavigateTo("/"); // Página principal
                return true;
            }

            return false;
        }

        // Método de logout
        public void Logout()
        {
            IsAuthenticated = false;
            // Redirigir a la página de login después de cerrar sesión
            _navigationManager.NavigateTo("/login");
        }

        // Método de comprobación de autenticación
        public async Task CheckAuthentication()
        {
            // Aquí podrías agregar lógica adicional para verificar si el usuario sigue autenticado
            // como, por ejemplo, revisar un token de sesión o hacer una llamada a la API
            await Task.CompletedTask;  // Esto es solo un marcador de posición si no haces nada asincrónico
        }

        // Método de redirección si ya está autenticado
        public void RedirectToHome()
        {
            if (IsAuthenticated)
            {
                _navigationManager.NavigateTo("/");
            }
        }
    }
}
