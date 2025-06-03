using ClinicaFisioterapiaApi.Data;
using Microsoft.EntityFrameworkCore;
using ClinicaFisioterapiaApi.Interfaces.Clinic;
using ClinicaFisioterapiaApi.Repositories;
using ClinicaFisioterapiaApi.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IClinicRepository, ClinicRepository>();
builder.Services.AddScoped<IClinicService, ClinicService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .LogTo(Console.WriteLine, LogLevel.Information) 
        .EnableSensitiveDataLogging()); 


var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();