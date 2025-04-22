using BI_TMA.API.EndPoints;
using BI_TMA.Shared.DB.Banco;
using BI_TMA.Shared.Models.Modelos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

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

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.Run();
