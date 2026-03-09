using Microsoft.EntityFrameworkCore;
using Embarcaciones.DAL.DataContext;
using Embarcaciones.Models;
using Embarcaciones.DAL.Repositorio;
using Embarcaciones.BLL.Service;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Servicios
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<EmbarcacionesBDContext>(x =>
    x.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL")));

builder.Services.AddScoped<IPersonaRepositorio, PersonaRepositorio>();
builder.Services.AddScoped<IGenericRepositorio<Departamento>, DepartamentoRepositorio>();
builder.Services.AddScoped<IGenericRepositorio<Municipio>, MunicipioRepositorio>();

builder.Services.AddScoped<IPersonaService, PersonaService>();
builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();
builder.Services.AddScoped<IMunicipioService, MunicipioService>();

builder.Services.AddScoped<ICatalogoRepositorio, CatalogoRepositorio>();
builder.Services.AddScoped<ICatalogoValorRepositorio, CatalogoValorRepositorio>();
builder.Services.AddScoped<ICatalogoValorService, CatalogoValorService>();
builder.Services.AddScoped<ICatalogoService, CatalogoService>();

builder.Services.AddScoped<ICuentaRepositorio, CuentaRepositorio>();
builder.Services.AddScoped<ICuentaService, CuentaService>();
builder.Services.AddScoped<IUnidadMedidaRepositorio, UnidadMedidaRepositorio>();
builder.Services.AddScoped<IUnidadMedidaService, UnidadMedidaService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/InicioSesion";
        options.LogoutPath = "/Login/CerrarSesion";
        options.AccessDeniedPath = "/Login/AccesoDenegado";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // debe ir antes de UseAuthorization
app.UseAuthorization();

// Rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=InicioSesion}");

app.Run();
