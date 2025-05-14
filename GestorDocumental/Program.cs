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
using Microsoft.AspNetCore.Components.Server.Circuits;
using GestorDocumental.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

//limite de tamaño de los archivos 
builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 50 * 1024 * 1024;
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 50 * 1024 * 1024; 
});

builder.Services.AddSignalR(e => {
    e.MaximumReceiveMessageSize = 50 * 1024 * 1024;
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

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.Secure = CookieSecurePolicy.Always;  // Las cookies deben ser siempre seguras
    options.MinimumSameSitePolicy = SameSiteMode.None;  // Permite que las cookies sean enviadas en solicitudes cross-site
});

builder.Services.Configure<LogSettings>(
    builder.Configuration.GetSection("LogSettings"));

//Retencion documental 
builder.Services.AddSingleton<RetencionDocumentalService>();

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

builder.Services.AddScoped<IEtiquetasRepository, EtiquetasRepository>();
builder.Services.AddScoped<IEtiquetasService, EtiquetasService>();

builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddScoped<IGrupoRepository, GrupoRepository>();
builder.Services.AddScoped<IGrupoService, GrupoService>();

builder.Services.AddScoped<ILogRepository, LogRepository>();
builder.Services.AddScoped<ILogService, LogService>();

builder.Services.AddScoped<IReportingRepository, ReportingRepository>();
builder.Services.AddScoped<IReportingService, ReportingService>();




// Servicio para la autenticación de usuario
builder.Services.AddScoped<AuthService>();


// Servicios de Radzen para UI
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<ProtectedSessionStorage>();

builder.Services.AddAntiforgery(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

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
app.UseCookiePolicy();

// Mapeo de componentes Razor
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

//Necesario para que se ejecute el servicio de gestor documental
app.Services.GetRequiredService<RetencionDocumentalService>();

app.Run();
