namespace CustomerServiceCampaign.Modules.Users.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<User?> GetAsync(string identityId, CancellationToken cancellationToken = default);
    
    Task<UserWithPermissions?> GetUserPermissions(string identityId, CancellationToken cancellationToken = default);
    
    void Insert(User user);
}
