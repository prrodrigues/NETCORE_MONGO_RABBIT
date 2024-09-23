using System.Reflection;
using Microsoft.OpenApi.Models;
using Test.RentMotorCycles.Domain.Entity;
using Test.RentMotorCycles.Domain.Repository;
using Test.RentMotorCycles.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opt =>
{

    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Sistema de Manutenção de Motos", Version = "v1" });
    opt.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opt.IncludeXmlComments(xmlPath);
});

builder.Services.AddTransient<IEntregadorService, EntregadorService>();
builder.Services.AddTransient<IMotoService, MotoService>();
builder.Services.AddTransient<ILocacaoService, LocacaoService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
