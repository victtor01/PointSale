using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PointSaleApi.src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Services;
using PointSaleApi.Src.Infra.Api.Middlewares;
using PointSaleApi.Src.Infra.Config;
using PointSaleApi.Src.Infra.Database;
using PointSaleApi.Src.Infra.Interfaces;
using PointSaleApi.src.Infra.Repositories;
using PointSaleApi.Src.Infra.Repositories;
using PointSaleApi.Src.Infra.utils;

namespace PointSaleApi.Src.Infra.Extensions;

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
            .WithOrigins("http://10.220.0.8:3000")
            .WithOrigins("http://10.220.0.8:8000")
            .WithOrigins("http://10.0.2.2")
            .WithOrigins("http://10.220.0.8:5039")
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
    // --managers--
    services.AddScoped<IManagersRepository, ManagersRepository>();
    services.AddScoped<IManagersService, ManagersService>();
    // --stores--
    services.AddScoped<IStoresRepository, StoresRepository>();
    services.AddScoped<IStoresService, StoresService>();
    // --tables--
    services.AddScoped<ITablesRepository, TablesRepository>();
    services.AddScoped<ITablesService, TablesService>();
    // --auth--
    services.AddScoped<IAuthService, AuthService>();
    // --products--
    services.AddScoped<IProductsRepository, ProductsRepository>();
    services.AddScoped<IProductsService, ProductsService>();
    // --orders--
    services.AddScoped<IOrdersService, OrdersService>();
    services.AddScoped<IOrdersRepository, OrdersRepository>();
    services.AddScoped<IFindOrdersService, FindOrdersService>();
    services.AddScoped<IOrdersCauculator, OrdersCauculator>();
    // --orders-products--
    services.AddScoped<IOrdersProductsService, OrdersProductsService>();
    services.AddScoped<IOrdersProductsRepository, OrdersProductsRepository>();
    // --employees--
    services.AddScoped<IEmployeesService, EmployeesService>();
    services.AddScoped<IEmployeeRepository, EmployeesRepository>();
    // --products-options--
    services.AddScoped<IOptionsProductsRepository, OptionsProductsRepository>();
    // --employee-positions--
    services.AddScoped<IPositionsRepository, PositionsRepository>();
    services.AddScoped<IEmployeePositionsService, EmployeePositionsService>();
    // --outers--
    services.AddScoped<ISessionService, SessionService>();
    services.AddSingleton<IJwtService, JwtService>();
    services.AddScoped<ITokenValidator, TokenValidator>();
    // --permissions--
    services.AddScoped<IPermissionsService, PermissionsService>();
  }
};

public static class ConfigureToResponseJsonExtension
{
  public static void ConfigureToResponseJson(this IServiceCollection services)
  {
    services.AddMvc()
      .AddJsonOptions(
        options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
      );
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
      .AddJwtBearer(options => { options.TokenValidationParameters = validationParameters; });
  }
}