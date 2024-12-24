using Microsoft.AspNetCore.Authentication.Cookies;
using PointSaleApi.Src.Infra.Api.Middlewares;
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
builder.ConfigureDatabase();
builder.ConfigureAuthentication();

Log.Logger = new LoggerConfiguration()
  .WriteTo.Console(theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Code)
  .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
  .CreateLogger();

builder.Host.UseSerilog();

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
