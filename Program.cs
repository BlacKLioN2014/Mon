using Microsoft.AspNetCore.Identity;
using Mon.Data;
using Microsoft.EntityFrameworkCore;
using Mon.Repository.IRepository;
using Mon.Repository;
using Mon.Mappers;
using Mon.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add Soporte para base de datos
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
                  opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")));

//Add Soporte para autenticacion con .net identity
builder.Services.AddIdentity<AppUsuario, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

//Add soporte auto mapper
builder.Services.AddAutoMapper(typeof(UserMappers));

//Add soporte repositorio
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
