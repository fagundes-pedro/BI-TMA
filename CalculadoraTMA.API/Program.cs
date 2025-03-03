using Calculadora_de_TMA.Banco;
using Calculadora_de_TMA.Modelos;
using System.Security.Cryptography.Xml;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

app.MapGet("/", () =>
{
    var assistneteDal = new DAL<Assistente>(new CalculadoraDeTmaContext());
    return assistneteDal.Listar();
});

app.Run();
