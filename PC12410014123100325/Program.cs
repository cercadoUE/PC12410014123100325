using Microsoft.EntityFrameworkCore;
using PC1.CORE.Core.Entities;

var builder = WebApplication.CreateBuilder(args);

// 1. Registrar AMBOS controladores ubicados en la biblioteca de clases
builder.Services.AddControllers()
    .AddApplicationPart(typeof(PC1.CORE.Controllers.TipoServicioController).Assembly)
    .AddApplicationPart(typeof(PC1.CORE.Controllers.OrdenServicioController).Assembly);

// 2. Conexión a la base de datos leyendo el JSON que acabamos de corregir
builder.Services.AddDbContext<TallerMecanicoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. Registrar el Repositorio de la Pregunta 5 para OrdenServicio
builder.Services.AddScoped<PC1.CORE.Core.Interfaces.IOrdenServicioRepository, PC1.CORE.Infrastructure.Repositories.OrdenServicioRepository>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
app.Run();