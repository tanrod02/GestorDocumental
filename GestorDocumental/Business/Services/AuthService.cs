using GestorDocumental.Business.Interfaces;
using GestorDocumental.Data.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage; // Agregar esta librería
using System.Threading.Tasks;

public class AuthService
{
    private readonly IUsuarioService _usuarioService;
    private readonly NavigationManager _navigationManager;
    private readonly ProtectedSessionStorage _sessionStorage; // Usar SessionStorage

    private Usuario _usuarioActual;
    public bool IsAuthenticated { get; private set; }

    public AuthService(IUsuarioService usuarioService, NavigationManager navigationManager, ProtectedSessionStorage sessionStorage)
    {
        _usuarioService = usuarioService;
        _navigationManager = navigationManager;
        _sessionStorage = sessionStorage;
        IsAuthenticated = false;
    }

    public async Task<bool> LoginAsync(string correo, string contraseña)
    {
        var usuario = await _usuarioService.IniciarSesionAsync(correo, contraseña);
        if (usuario != null)
        {
            Console.WriteLine($"Usuario autenticado: {usuario.Correo}");
            IsAuthenticated = true;
            _usuarioActual = usuario;

            // Guardar en sesión
            await _sessionStorage.SetAsync("usuario", usuario);
            return true;
        }
        Console.WriteLine("Credenciales incorrectas en AuthService.");
        return false;
    }

    public async Task<Usuario> ObtenerUsuarioActualAsync()
    {
        if (_usuarioActual == null)
        {
            // Intentar recuperar la sesión
            var result = await _sessionStorage.GetAsync<Usuario>("usuario");
            _usuarioActual = result.Success ? result.Value : null;
            IsAuthenticated = _usuarioActual != null;
        }
        return _usuarioActual;
    }

    public async Task Logout()
    {
        IsAuthenticated = false;
        _usuarioActual = null;

        // Borrar la sesión
        await _sessionStorage.DeleteAsync("usuario");

        _navigationManager.NavigateTo("/");
    }
}





//using GestorDocumental.Business.Interfaces;
//using Microsoft.AspNetCore.Components;
//using System.Threading.Tasks;

//namespace GestorDocumental.Business.Services
//{
//    public class AuthService
//    {
//        private readonly IUsuarioService _usuarioService;
//        private readonly NavigationManager _navigationManager;

//        // Estado de la autenticación en memoria
//        public bool IsAuthenticated { get; private set; }

//        public AuthService(IUsuarioService usuarioService)
//        {
//            _usuarioService = usuarioService;
//            IsAuthenticated = false; // El usuario no está autenticado al inicio
//        }

//        // Método de login
//        public async Task<bool> LoginAsync(string correo, string contraseña)
//        {
//            var usuario = await _usuarioService.IniciarSesionAsync(correo, contraseña);

//            if (usuario != null)
//            {
//                // Verifica la comparación del hash de la contraseña
//                Console.WriteLine($"Usuario encontrado: {usuario.Correo}");
//                IsAuthenticated = true;
//                return true;
//            }

//            Console.WriteLine("Credenciales incorrectas en auth.");
//            return false;
//        }


//        // Método de logout
//        public void Logout()
//        {
//            this.IsAuthenticated = false; // Asegúrate de acceder a la propiedad a través de la instancia
//            // Redirigir a la página de login después de cerrar sesión
//            _navigationManager.NavigateTo("/login");
//        }

//        // Método de comprobación de autenticación
//        public async Task CheckAuthentication()
//        {
//            // Aquí podrías agregar lógica adicional para verificar si el usuario sigue autenticado
//            // como, por ejemplo, revisar un token de sesión o hacer una llamada a la API
//            await Task.CompletedTask;  // Esto es solo un marcador de posición si no haces nada asincrónico
//        }

//        // Método de redirección si ya está autenticado
//        public void RedirectToHome()
//        {
//            if (this.IsAuthenticated) // Asegúrate de usar la instancia aquí también
//            {
//                _navigationManager.NavigateTo("/");
//            }
//        }


//    }
//}
