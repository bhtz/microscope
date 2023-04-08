namespace Microscope.ExternalSystems.Services;

public interface IUserService
{
    Task<IEnumerable<DomainUser>> GetUsersAsync(int limit);
    Task<IEnumerable<DomainUser>> SearchUsersAsync(string search);
}

public class DomainUser
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
}
