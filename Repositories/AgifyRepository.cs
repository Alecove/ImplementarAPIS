using ExternalDataApi.Models;

namespace ExternalDataApi.Repositories;

public class AgifyRepository : IAgifyRepository
{
    private readonly HttpClient _httpClient;

    public AgifyRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<int?> GetEstimatedAgeAsync(string name)
    {
        try
        {
            // La API funciona así: https://api.agify.io?name=michael
            var response = await _httpClient.GetFromJsonAsync<AgifyResponse>($"?name={name}");
            return response?.Age;
        }
        catch
        {
            // Si falla la API de edad, no rompemos todo, simplemente devolvemos null
            return null;
        }
    }
}