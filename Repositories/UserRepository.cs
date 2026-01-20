using ExternalDataApi.Models;

namespace ExternalDataApi.Repositories;

public class UserRepository : IUserRepository
{
    private readonly HttpClient _httpClient;

    public UserRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<User>> GetAllAsync()
    {
        var users = await _httpClient.GetFromJsonAsync<List<User>>("users");
        return users ?? new List<User>();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        try 
        {
            return await _httpClient.GetFromJsonAsync<User>($"users/{id}");
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }public async Task<List<Post>> GetPostsByUserIdAsync(int userId)
    {
        // La API de JSONPlaceholder tiene esta ruta nativa para filtrar posts por usuario
        var posts = await _httpClient.GetFromJsonAsync<List<Post>>($"users/{userId}/posts");
        return posts ?? new List<Post>();
    }
}
