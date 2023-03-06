using System.Text;
using Final.Base.Jwt;
using Final.Data.Context;
using Final_Case.Extension;
using Final_Case.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

//Token
var jwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>();
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));
builder.Services.AddJwtBearerAuthentication(jwtConfig);

//Token içini globalde görmek için.
builder.Services.AddHttpContextAccessor();

//------------------------------------------------------//
//Servisi ekledik.
builder.Services.AddServiceDI();
builder.Services.AddDbContextDI(builder.Configuration);
builder.Services.AddMongoContextDI(builder.Configuration);
//------------------------------------------------------//

//------------------------------------------------------//
// Initialize Logger
builder.Host.UseSerilog();
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
Log.Information("Application is starting.");
//------------------------------------------------------//

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//delete this
//builder.Services.AddSwaggerGen();
//new Swagger
builder.Services.AddCustomizeSwagger();

var app = builder.Build();

//Run DataGenerator
using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DataGenerator.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

//custom Middlewera burada tanımlayıp çalıştırıyoruz.
app.UseCustomExceptionMiddle();

app.MapControllers();

app.Run();

