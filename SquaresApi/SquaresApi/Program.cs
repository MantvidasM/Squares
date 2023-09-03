using Microsoft.EntityFrameworkCore;
using SquaresApi.Data;
using SquaresApi.Interfaces;
using SquaresApi.Repository;
using SquaresApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApiDbContext>(options => options.UseSqlite(
    builder.Configuration.GetConnectionString("localDb")));

builder.Services.AddScoped<ISquaresService, SquaresService>();
builder.Services.AddScoped<IPointRepository, PointRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

