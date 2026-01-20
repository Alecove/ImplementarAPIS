using Microsoft.AspNetCore.Mvc;
using ExternalDataApi.Services;

namespace ExternalDataApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IJsonPlaceholderService _service;

    // Inyectamos la interfaz, no la clase concreta (Principio de Inversión de Dependencias)
    public UsersController(IJsonPlaceholderService service)
    {
        _service = service;
    }

    // GET: api/users
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = await _service.GetUsersAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            // Es buena práctica capturar errores de red externos para no romper tu API
            return StatusCode(500, new { message = "Error obteniendo datos externos", error = ex.Message });
        }
    }
    // GET: api/users/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _service.GetUserByIdAsync(id);

        if (user == null)
        {
            return NotFound(new { message = $"El usuario con ID {id} no existe." });
        }

        return Ok(user);
    }
    // GET: api/users/1/enriched
    [HttpGet("{id}/enriched")]
    public async Task<IActionResult> GetEnrichedUser(int id)
    {
        var enrichedUser = await _service.GetEnrichedUserAsync(id);

        if (enrichedUser == null)
        {
            return NotFound(new { message = "Usuario no encontrado." });
        }

        return Ok(enrichedUser);
    }
    // GET: api/users/1/posts
    [HttpGet("{id}/posts")]
    public async Task<IActionResult> GetUserPosts(int id)
    {
        // Opcional: Podrías verificar primero si el usuario existe, 
        // pero la API externa simplemente devolverá una lista vacía si no hay posts.
        var posts = await _service.GetUserPostsAsync(id);
        return Ok(posts);
    }
}