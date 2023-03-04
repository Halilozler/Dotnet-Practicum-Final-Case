using Final.Data.Context;
using Final_Case.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//------------------------------------------------------//
//Servisi ekledik.
builder.Services.AddServiceDI();
builder.Services.AddDbContextDI(builder.Configuration);
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

app.MapControllers();

app.Run();

