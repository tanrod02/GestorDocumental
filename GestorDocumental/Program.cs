using GestorDocumental.Business.Interfaces;
using GestorDocumental.Business.Services;
using GestorDocumental.Components;
using GestorDocumental.Data;
using GestorDocumental.Data.Interfaces;
using GestorDocumental.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Obtener la cadena de conexión desde la configuración
var connectionString = builder.Configuration.GetConnectionString("GestorDocumentalConnection");

// Configurar DbContextFactory con SQL Server
builder.Services.AddDbContextFactory<GestorDocumentalDbContext>(options =>
    options.UseSqlServer(connectionString));

// Agregar servicios al contenedor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRadzenComponents();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Inyección de dependencias para repositorios y servicios
builder.Services.AddScoped<IArchivoRepository, ArchivoRepository>();
builder.Services.AddScoped<IArchivoService, ArchivoService>();

builder.Services.AddScoped<ICursoRepository, CursoRepository>();
builder.Services.AddScoped<ICursoService, CursoService>();

// Servicios de Radzen para UI
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<DialogService>();

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
