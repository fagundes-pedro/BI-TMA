using Calculadora_de_TMA.Banco;
using Calculadora_de_TMA.Modelos;
using CalculadoraTMA.API.EndPoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<CalculadoraDeTmaContext>();
builder.Services.AddTransient<DAL<Assistente>>();
builder.Services.AddTransient<DAL<Linha>>();
builder.Services.AddTransient<DAL<LinhaAssistente>>();
builder.Services.AddTransient<DAL<Chamada>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.AddEndPointAssistentes();
app.AddEndPointLinhas();
app.AddEndPointChamadas();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
