using ExternalDataApi.Models;
using ExternalDataApi.Repositories;

namespace ExternalDataApi.Services;

public class JsonPlaceholderService : IJsonPlaceholderService
{
    private readonly IUserRepository _userRepository;
    private readonly IAgifyRepository _agifyRepository;

    // Inyectamos AMBOS repositorios en el constructor
    public JsonPlaceholderService(IUserRepository userRepository, IAgifyRepository agifyRepository)
    {
        _userRepository = userRepository;
        _agifyRepository = agifyRepository;
    }

    // Método 1: Obtener todos (Passthrough al repo de usuarios)
    public async Task<List<User>> GetUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    // Método 2: Obtener por ID (Passthrough al repo de usuarios)
    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    // Método 3: LÓGICA DE AGREGACIÓN (Cruza datos de las dos APIs)
    public async Task<EnrichedUserDto?> GetEnrichedUserAsync(int id)
    {
        // 1. Buscamos el usuario principal
        var user = await _userRepository.GetByIdAsync(id);
        
        if (user == null) 
        {
            return null;
        }

        // 2. Preparamos el nombre para la segunda API (Agify suele funcionar mejor con el primer nombre)
        var firstName = user.Name.Split(' ')[0];

        // 3. Consultamos la segunda API (Edad)
        var estimatedAge = await _agifyRepository.GetEstimatedAgeAsync(firstName);

        // 4. Construimos el objeto combinado (DTO)
        var enrichedUser = new EnrichedUserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Company = user.Company,
            EstimatedAge = estimatedAge // Dato externo enriquecido
        };

        return enrichedUser;
    }// Método 4: Obtener Posts del Usuario
    public async Task<List<Post>> GetUserPostsAsync(int id)
    {
        return await _userRepository.GetPostsByUserIdAsync(id);
    }
}
