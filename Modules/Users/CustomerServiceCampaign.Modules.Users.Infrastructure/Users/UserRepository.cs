using CustomerServiceCampaign.Modules.Users.Domain.Users;
using CustomerServiceCampaign.Modules.Users.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CustomerServiceCampaign.Modules.Users.Infrastructure.Users;

internal sealed class UserRepository(UsersDbContext context) : IUserRepository
{
    public async Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Users.SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
    }
    
    public async Task<User?> GetAsync(string identityId, CancellationToken cancellationToken = default)
    {
        return await context.Users.SingleOrDefaultAsync(u => u.IdentityId == identityId, cancellationToken);
    }

    public async Task<UserWithPermissions?> GetUserPermissions(string identityId, CancellationToken cancellationToken = default)
    {
         FormattableString sql =
            $"""
             SELECT DISTINCT
                 u.id AS UserId,
                 rp.PermissionCode AS Permission
             FROM users.users u
             JOIN users.user_roles ur ON ur.UserId = u.id
             JOIN users.role_permissions rp ON rp.RoleName = ur.role_name
             WHERE u.IdentityId = {identityId}
             """;

         var rows = await context.Database
             .SqlQuery<UserWithPermission>(sql)
             .ToListAsync(cancellationToken);

         if (rows.Count == 0)
         {
             return null;
         }

         var userId = rows[0].UserId;
         var permissions = rows
             .Select(r => r.Permission)
             .ToHashSet(StringComparer.OrdinalIgnoreCase);

         return new UserWithPermissions(userId, permissions);
    }

    private sealed record UserWithPermission(Guid UserId, string Permission);
    
    public void Insert(User user)
    {
        foreach (Role role in user.Roles)
        {
            context.Attach(role);
        }

        context.Users.Add(user);
    }
}