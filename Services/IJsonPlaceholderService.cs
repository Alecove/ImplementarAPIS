using ExternalDataApi.Models;

namespace ExternalDataApi.Services;

public interface IJsonPlaceholderService
{
    Task<List<User>> GetUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<EnrichedUserDto?> GetEnrichedUserAsync(int id);
    Task<List<Post>> GetUserPostsAsync(int id); // <--- NUEVO
}