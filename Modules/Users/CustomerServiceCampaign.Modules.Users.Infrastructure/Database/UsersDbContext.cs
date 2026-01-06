using CustomerServiceCampaign.Common.Infrastructure.Inbox;
using CustomerServiceCampaign.Common.Infrastructure.Outbox;
using CustomerServiceCampaign.Modules.Users.Application.Abstractions.Data;
using CustomerServiceCampaign.Modules.Users.Domain.Users;
using CustomerServiceCampaign.Modules.Users.Infrastructure.Users;
using Microsoft.EntityFrameworkCore;

namespace CustomerServiceCampaign.Modules.Users.Infrastructure.Database;

public sealed class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Users);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionConfiguration());
    }
}
