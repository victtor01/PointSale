using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PointSaleApi.Src.Infra.Api.Middlewares;
using PointSaleApi.Src.Infra.Database;
using PointSaleApi.src.Infra.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddCorsPolicy();
builder.Services.ConfigureValidationBehavior();

Log.Logger = new LoggerConfiguration()
  .WriteTo.Console(theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Code)
  .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
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

app.UseCors("AllowSpecificOriginWithCredentials");
app.UseMiddleware<ErrorMiddleware>();
app.UseMiddleware<SessionMiddleware>();
app.UseAuthentication();
app.MapControllers();
app.Run();
