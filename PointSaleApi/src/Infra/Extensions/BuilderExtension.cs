using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PointSaleApi.src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Services;
using PointSaleApi.Src.Infra.Config;
using PointSaleApi.Src.Infra.Database;
using PointSaleApi.src.Infra.Repositories;
using PointSaleApi.Src.Infra.Repositories;

namespace PointSaleApi.Src.Infra.Extensions
{
  public static class CorsConfig
  {
    public static void AddCorsPolicy(this IServiceCollection services)
    {
      services.AddCors(options =>
        options.AddPolicy(
          "AllowSpecificOriginWithCredentials",
          policy =>
          {
            policy
              .WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
          }
        )
      );
    }
  }

  public static class DependencyInjection
  {
    public static void AddApplicationServices(this IServiceCollection services)
    {
      services.AddScoped<IManagersRepository, ManagersRepository>();
      services.AddScoped<IStoresRepository, StoresRepository>();
      services.AddScoped<ITablesRepository, TablesRepository>();
      services.AddScoped<IAuthService, AuthService>();
      services.AddScoped<IManagersService, ManagersService>();
      services.AddScoped<IStoresService, StoresService>();
      services.AddScoped<IProductsRepository, ProductsRepository>();
      services.AddScoped<ITablesService, TablesService>();
      services.AddSingleton<ISessionService, SessionService>();
      services.AddSingleton<IJwtService, JwtService>();
    }
  };

  public static class ApiBehaviorExtensions
  {
    public static void ConfigureValidationBehavior(this IServiceCollection services)
    {
      services.Configure<ApiBehaviorOptions>(options =>
      {
        options.InvalidModelStateResponseFactory = context =>
        {
          var errors =
            context
              .ModelState.Where(ms => ms.Value?.Errors?.Any() == true)
              .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
              ) ?? null;

          throw new BadRequestException("One or more validation errors occurred.", errors);
        };
      });
    }
  }

  public static class ConnectToDatabase
  {
    public static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
      builder.Services.AddDbContext<DatabaseContext>(options =>
      {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
      });
    }
  }

  public static class Authentication
  {
    public static void ConfigureAuthentication(this WebApplicationBuilder builder)
    {
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
    }
  }
}
