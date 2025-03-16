using BaseLibrary.DTOs.AdminFunctionDTOs;
using BaseLibrary.DTOs.Dietitian;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;
using ServerLibrary.Repositories.Implementations;
using ServerLibrary.Repositories.Implementations.AdminFunctions;
using ServerLibrary.Repositories.Implementations.Dietitian;
using ServerLibrary.Repositories.Implementations.Patient;
using ServerLibrary.Repositories.Implementations.Seed;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JwtSection>(builder.Configuration.GetSection("JwtSection"));
var jwtSection = builder.Configuration.GetSection(nameof(JwtSection)).Get<JwtSection>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = jwtSection!.Issuer,
        ValidAudience = jwtSection!.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection.Key!)),
    };
});

// Authenticatation DI
builder.Services.Configure<JwtSection>(builder.Configuration.GetSection("JwtSection"));
builder.Services.AddScoped<IUserAccount, UserAccountRepository>();
builder.Services.AddScoped<IUserRoles, RolesRepository>();
builder.Services.AddScoped<JWThelper>();


// Admin function DI
builder.Services.AddScoped<IGenericRepository<AllergyDTO>, AllergyRepository>();
builder.Services.AddScoped<IGenericRepository<DiseaseDTO>, DiseaseRepository>();
builder.Services.AddScoped<IGenericRepository<IngredientDTO>, IngredientRepository>();
builder.Services.AddScoped<IGenericRepository<MedicationDTO>, MedicationRepository>();
builder.Services.AddScoped<IGenericRepository<FoodsDTO>, FoodRepository>();

// Patient DI
builder.Services.AddScoped<IPatientAssignment<PatientDTO>, AdminPatientRepository>();
builder.Services.AddScoped<IPatient, PatientRepository>();

// Dietitan DI
builder.Services.AddScoped<IAvailableDietitian<AvailableDietDTO>, AvailableDietitianRepository>();

// Profile DI
builder.Services.AddScoped<IProfile, ProfileRepository>();

// Seed DI
builder.Services.AddScoped<ISeeder, DiseaseSeed>();
builder.Services.AddScoped<ISeeder, AllergySeed>();
builder.Services.AddScoped<ISeeder, RoleSeed>();
builder.Services.AddScoped<ISeeder, MedicationSeed>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorWasm",
        builder => builder
        .WithOrigins("http://localhost:5146", "https://localhost:7046")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazorWasm");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
