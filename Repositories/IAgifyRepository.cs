namespace ExternalDataApi.Repositories;

public interface IAgifyRepository
{
    Task<int?> GetEstimatedAgeAsync(string name);
}