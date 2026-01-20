namespace ExternalDataApi.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty; // Nuevo
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;    // Nuevo
    public string Website { get; set; } = string.Empty;
    
    // Propiedades complejas (Objetos anidados)
    public Address? Address { get; set; }
    public Company? Company { get; set; }
}

public class Address
{
    public string Street { get; set; } = string.Empty;
    public string Suite { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Zipcode { get; set; } = string.Empty;
    public Geo? Geo { get; set; }
}

public class Geo
{
    public string Lat { get; set; } = string.Empty;
    public string Lng { get; set; } = string.Empty;
}

public class Company
{
    public string Name { get; set; } = string.Empty;
    public string CatchPhrase { get; set; } = string.Empty;
    public string Bs { get; set; } = string.Empty;
}