using ClinicaFisioterapiaApi.Infrastructure.Data;
using ClinicaFisioterapiaApi.Infrastructure.Repositories.Users;
using ClinicaFisioterapiaApi.Infrastructure.Repositories.Clinics;
using ClinicaFisioterapiaApi.Application.Interfaces;
using ClinicaFisioterapiaApi.Application.Interfaces.Repositories;
using ClinicaFisioterapiaApi.Application.UseCases.Users;
using ClinicaFisioterapiaApi.Application.UseCases.Clinics;
using ClinicaFisioterapiaApi.Infrastructure.Services;
using ClinicaFisioterapiaApi.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using ClinicaFisioterapiaApi.Infrastructure.Repositories.People;
using ClinicaFisioterapiaApi.Common.Validators;
using ClinicaFisioterapiaApi.Application.UseCases.People;

var builder = WebApplication.CreateBuilder(args);

// 🔐 Configura mapeamento da seção JwtSettings para injeção via IOptions<JwtSettings>
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

// ✅ Controladores + Suporte para enums no JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

// 🗄️ Banco de dados PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🧱 Repositórios
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IClinicRepository, ClinicRepository>(); 
builder.Services.AddScoped<PersonRepository>(); // People

// 🔐 Serviços JWT + RefreshToken
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();

// ✅ Validadores
builder.Services.AddScoped<ICpfValidator, CpfValidator>(); // CPF Validator

// 🧠 UseCases (Users)
builder.Services.AddScoped<CreateUserUseCase>();
builder.Services.AddScoped<DeleteUserUseCase>();
builder.Services.AddScoped<GetUserByIdUseCase>();
builder.Services.AddScoped<GetUsersPagedUseCase>();
builder.Services.AddScoped<LoginUserUseCase>();
builder.Services.AddScoped<RefreshTokenUseCase>();
builder.Services.AddScoped<UpdateUserUseCase>();

// 🧠 UseCases (Clinics)
builder.Services.AddScoped<CreateClinicUseCase>();
builder.Services.AddScoped<GetClinicByIdUseCase>();
builder.Services.AddScoped<GetClinicsPagedUseCase>();
builder.Services.AddScoped<UpdateClinicUseCase>();
builder.Services.AddScoped<DeleteClinicUseCase>();

// 🧠 UseCases (People)
builder.Services.AddScoped<GetAllPeopleUseCase>();
builder.Services.AddScoped<GetPersonByIdUseCase>();
builder.Services.AddScoped<CreatePersonUseCase>();
builder.Services.AddScoped<UpdatePersonUseCase>();
builder.Services.AddScoped<DeletePersonUseCase>();



// 🔁 AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// 🌐 CORS (para testes — restrinja em produção)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// 🔐 Autenticação JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["Secret"] ?? throw new Exception("JWT Secret não configurado.");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
