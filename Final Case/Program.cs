using Final.Data.Context;
using Final_Case.Extension;
using Final_Case.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

//------------------------------------------------------//
//Servisi ekledik.
builder.Services.AddServiceDI();
builder.Services.AddDbContextDI(builder.Configuration);
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
builder.Services.AddSwaggerGen();

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

app.UseAuthorization();

//custom Middlewera burada tanımlayıp çalıştırıyoruz.
app.UseCustomExceptionMiddle();

app.MapControllers();

app.Run();

