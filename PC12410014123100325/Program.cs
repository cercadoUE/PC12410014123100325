using Microsoft.EntityFrameworkCore;
using PC.CORE.Core.Entities;
using PC.CORE.Core.Interfaces;
using PC.CORE.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(PC.CORE.Controllers.TipoServicioController).Assembly)
    .AddApplicationPart(typeof(PC.CORE.Controllers.OrdenServicioController).Assembly);

builder.Services.AddDbContext<TallerMecanicoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IOrdenServicioRepository, OrdenServicioRepository>();

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
