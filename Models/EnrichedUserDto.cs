namespace ExternalDataApi.Models;

public class EnrichedUserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int? EstimatedAge { get; set; } // <--- El dato nuevo de la API 2
    public Company? Company { get; set; }
}

// Clase auxiliar para leer la respuesta de Agify.io
public class AgifyResponse
{
    public int? Age { get; set; }
    public string Name { get; set; } = string.Empty;
}