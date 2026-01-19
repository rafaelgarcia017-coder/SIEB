using Microsoft.EntityFrameworkCore;
using Embarcaciones.DAL.DataContext;
using Embarcaciones.Models;
using Embarcaciones.DAL.Repositorio;
using Embarcaciones.BLL.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<EmbarcacionesBDContext>(
    x=> x.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL")));

builder.Services.AddScoped<IGenericRepositorio<Persona>, PersonaRepositorio>() ;
builder.Services.AddScoped<IPersonaService, PersonaService>();

builder.Services.AddScoped<ICuentaRepositorio, CuentaRepositorio>();// Servicio
builder.Services.AddScoped<ICuentaService, CuentaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=InicioSesion}");

app.Run();

// Repositorio

