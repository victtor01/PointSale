using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PointSaleApi.src.Core.Application.Interfaces.AuthInterfaces;
using PointSaleApi.src.Core.Application.Interfaces.JwtInterfaces;
using PointSaleApi.src.Core.Application.Interfaces.ManagersInterfaces;
using PointSaleApi.src.Core.Application.Interfaces.SessionInterfaces;
using PointSaleApi.src.Core.Application.Interfaces.StoresInterfaces;
using PointSaleApi.src.Core.Application.Services;
using PointSaleApi.src.Infra.Api.Middlewares;
using PointSaleApi.src.Infra.Database;
using PointSaleApi.src.Infra.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<IManagersService, ManagersService>();
builder.Services.AddScoped<IManagersRepository, ManagersRepository>();
builder.Services.AddScoped<IStoresService, StoresService>();
builder.Services.AddScoped<IStoresRepository, StoresRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddSingleton<ISessionService, SessionService>();
builder.Services.AddSingleton<IJwtService, JwtService>();

Log.Logger = new LoggerConfiguration()
  .WriteTo.Console(theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Code) // Tema com cores
  .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day) // Log em arquivo com rotação diária
  .CreateLogger();

builder.Host.UseSerilog();

var validationParameters = new TokenValidationParameters
{
  ValidateIssuer = true,
  ValidateAudience = true,
  ValidAudience = "teste",
  ValidIssuer = "teste",
  ValidateIssuerSigningKey = true,
  IssuerSigningKey = new SymmetricSecurityKey(
    Encoding.UTF8.GetBytes(builder.Configuration["jwt:secretKey"]!)
  ),
  ValidateLifetime = true,
  ClockSkew = TimeSpan.Zero,
};

builder.Services.AddSingleton(validationParameters);

builder
  .Services.AddAuthentication(options =>
  {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  })
  .AddJwtBearer(options =>
  {
    options.TokenValidationParameters = validationParameters;
  });

builder.Services.AddDbContext<DatabaseContext>(options =>
{
  options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseMiddleware<ErrorMiddleware>();
app.UseMiddleware<SessionMiddleware>();
app.UseAuthentication();
app.MapControllers();
app.Run();
