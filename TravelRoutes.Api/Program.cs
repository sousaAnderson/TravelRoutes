using Microsoft.OpenApi.Models;
using TravelRoutes.Application.Interfaces;
using TravelRoutes.Application.Services;
using TravelRoutes.Domain.Abstractions;
using TravelRoutes.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);


// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy.WithOrigins("http://localhost:4200") // Permite o Angular acessar a API
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //Adicionar Annotations
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Rota de Viagem",
        Version = "v1",
        Description = "Web API que determina a escolha da rota de viagem mais barata independente da quantidade de conexões.",
        Contact = new OpenApiContact
        {
            Name = "José Anderson Pereira de Sousa",
            Email = "japdesousa@gmail.com",
            Url = new Uri("https://github.com/sousaAnderson/")
        }
    });
});

//Injeção de dependencia
builder.Services.AddScoped<IRouteRepository, RouteRepository>();
builder.Services.AddScoped<IRouteService, RouteService>();

var app = builder.Build();

// Ativar o CORS na API
app.UseCors("AllowAngular");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
