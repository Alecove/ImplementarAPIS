using ExternalDataApi.Services;
using ExternalDataApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. Agregar servicios al contenedor.

builder.Services.AddControllers();

// Configuración para Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- INYECCIÓN DE DEPENDENCIAS ---

// A. Repositorio de Usuarios (JSONPlaceholder)
// Registramos el primer cliente HTTP tipado
builder.Services.AddHttpClient<IUserRepository, UserRepository>(client =>
{
    client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
});

// B. Repositorio de Edad (Agify) -> ¡ESTA ES LA LÍNEA QUE FALTABA!
// Registramos el segundo cliente HTTP tipado para arreglar el error
builder.Services.AddHttpClient<IAgifyRepository, AgifyRepository>(client =>
{
    client.BaseAddress = new Uri("https://api.agify.io/");
});

// C. Servicio de Negocio
// Registramos el servicio que orquesta los dos repositorios anteriores
builder.Services.AddScoped<IJsonPlaceholderService, JsonPlaceholderService>();

// ---------------------------------

var app = builder.Build();

// 2. Configurar el pipeline de peticiones HTTP.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();