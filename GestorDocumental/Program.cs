using GestorDocumental.Business.Interfaces;
using GestorDocumental.Business.Services;
using GestorDocumental.Components;
using GestorDocumental.Data;
using GestorDocumental.Data.Interfaces;
using GestorDocumental.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;
using Radzen;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

//limite de tamaño de los archivos 
builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 50 * 1024 * 1024; ; // 2 GB
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 50 * 1024 * 1024; ; // 2 GB
});



// Obtener la cadena de conexión desde la configuración
var connectionString = builder.Configuration.GetConnectionString("GestorDocumentalConnection");

// Configurar DbContextFactory con SQL Server
builder.Services.AddDbContextFactory<GestorDocumentalDbContext>(options =>
    options.UseSqlServer(connectionString));

// Agregar servicios al contenedor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddRadzenComponents();

// Inyección de dependencias para repositorios y servicios
builder.Services.AddScoped<IArchivoRepository, ArchivoRepository>();
builder.Services.AddScoped<IArchivoService, ArchivoService>();

builder.Services.AddScoped<ICursoRepository, CursoRepository>();
builder.Services.AddScoped<ICursoService, CursoService>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<IEstadisticaRepository, EstadisticaRepository>();
builder.Services.AddScoped<IEstadisticaService, EstadisticaService>();

builder.Services.AddScoped<ICarpetaRepository, CarpetaRepository>();
builder.Services.AddScoped<ICarpetaService, CarpetaService>();

// Servicio para la autenticación de usuario
builder.Services.AddScoped<AuthService>();


// Servicios de Radzen para UI
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<ProtectedSessionStorage>();

var app = builder.Build();

// Configuración del pipeline de la aplicación
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// Mapeo de componentes Razor
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
