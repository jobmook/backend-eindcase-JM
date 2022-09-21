using CursusApp.Backend.DataAccess;
using CursusApp.Backend.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<CursusDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CursusDbContext"));
}, ServiceLifetime.Transient);

builder.Services.AddTransient<CursusRepository>();
builder.Services.AddTransient<CursusInstantieRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("frontend", policy =>
    {
        policy.WithOrigins("https://localhost:4200", "http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseStaticFiles();

app.UseRouting();

app.UseCors("frontend");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
