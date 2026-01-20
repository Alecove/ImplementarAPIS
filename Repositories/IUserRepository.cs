using ExternalDataApi.Models;

namespace ExternalDataApi.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<List<Post>> GetPostsByUserIdAsync(int userId); // <--- NUEVO
}