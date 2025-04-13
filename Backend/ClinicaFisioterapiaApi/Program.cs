using ClinicaFisioterapiaApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .LogTo(Console.WriteLine, LogLevel.Information) 
        .EnableSensitiveDataLogging()); 


var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization(); 
app.MapControllers();
app.Run();