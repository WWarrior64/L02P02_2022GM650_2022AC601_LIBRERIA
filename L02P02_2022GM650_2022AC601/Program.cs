using L02P02_2022GM650_2022AC601.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Inyeccion por dependencias del string de conexion al contexto
builder.Services.AddDbContext<libreriaDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("libreriaDbConnection")
    )
);


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
    pattern: "{controller=Libreria}/{action=Autor}/{id?}");

app.Run();
