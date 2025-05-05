using GestorDocumental.Business.Interfaces;
using GestorDocumental.Data.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage; 
using System;
using System.Threading.Tasks;

public class AuthService
{
    private readonly IUsuarioService _usuarioService;
    private readonly NavigationManager _navigationManager;
    private readonly ProtectedSessionStorage _sessionStorage; 

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
        try
        {
            var usuario = await _usuarioService.IniciarSesionAsync(correo, contraseña);
            if (usuario != null)
            {
                Console.WriteLine($"Usuario autenticado: {usuario.Correo}");
                IsAuthenticated = true;
                _usuarioActual = usuario;

                await _sessionStorage.SetAsync("usuario", usuario);
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[LoginAsync] Error: {ex.Message}");
            return false;
        }
    }


    public async Task<Usuario> ObtenerUsuarioActualAsync()
    {
        if (_usuarioActual == null)
        {
            try
            {
                // Solo intentamos obtener la sesión si Blazor ya es interactivo
                var result = await _sessionStorage.GetAsync<Usuario>("usuario");

                // Verificamos si la recuperación fue exitosa
                if (result.Success)
                {
                    _usuarioActual = result.Value;
                    IsAuthenticated = true;
                    Console.WriteLine($"Usuario recuperado de sesión: {_usuarioActual.Nombre}");
                }
                else
                {
                    Console.WriteLine("No hay usuario en sesión.");
                    IsAuthenticated = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener usuario de la sesión: {ex.Message}");
                _usuarioActual = null;
                IsAuthenticated = false;
            }
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


