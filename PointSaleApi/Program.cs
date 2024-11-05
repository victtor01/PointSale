using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PointSaleApi.src.Core.Application.Interfaces.AuthInterfaces;
using PointSaleApi.src.Core.Application.Interfaces.JwtInterfaces;
using PointSaleApi.src.Core.Application.Interfaces.ManagersInterfaces;
using PointSaleApi.src.Core.Application.Interfaces.StoresInterfaces;
using PointSaleApi.src.Core.Application.Services;
using PointSaleApi.src.Infra.Api.Middlewares;
using PointSaleApi.src.Infra.Database;
using PointSaleApi.src.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<IManagersService, ManagersService>();
builder.Services.AddScoped<IManagersRepository, ManagersRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IStoresService, StoresService>();
builder.Services.AddScoped<IStoresRepository, StoresRepository>();

builder.Services.AddSingleton<IJwtService, JwtService>();

builder
  .Services.AddAuthentication(options =>
  {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  })
  .AddJwtBearer(options =>
  {
    options.TokenValidationParameters = new TokenValidationParameters
    {
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidateLifetime = true,
      IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(builder.Configuration["jwt:secretKey"]!)
      ),
      ClockSkew = TimeSpan.Zero,
    };
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
