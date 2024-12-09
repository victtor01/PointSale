using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Src.Core.Application.Interfaces.AuthInterfaces;
using PointSaleApi.Src.Core.Application.Interfaces.JwtInterfaces;
using PointSaleApi.Src.Core.Application.Interfaces.ManagersInterfaces;
using PointSaleApi.Src.Core.Application.Interfaces.SessionInterfaces;
using PointSaleApi.Src.Core.Application.Interfaces.StoresInterfaces;
using PointSaleApi.Src.Core.Application.Services;
using PointSaleApi.Src.Infra.Config;
using PointSaleApi.Src.Infra.Repositories;

namespace PointSaleApi.src.Infra.Extensions
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
      services.AddScoped<IManagersService, ManagersService>();
      services.AddScoped<IManagersRepository, ManagersRepository>();
      services.AddScoped<IStoresService, StoresService>();
      services.AddScoped<IStoresRepository, StoresRepository>();
      services.AddScoped<IAuthService, AuthService>();

      services.AddSingleton<ISessionService, SessionService>();
      services.AddSingleton<IJwtService, JwtService>();
    }
  }

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
}
