using CustomerServiceCampaign.Modules.Users.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerServiceCampaign.Modules.Users.Infrastructure.Users;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");

        builder.HasKey(p => p.Code);

        builder.Property(p => p.Code).HasMaxLength(100);

        builder.HasData(
            Permission.GetUser,
            Permission.ModifyUser,
            Permission.GetCampaign,
            Permission.CreateCampaign,
            Permission.ModifyCampaign,
            Permission.GetReward,
            Permission.CreateReward,
            Permission.ModifyReward
                );

        builder
            .HasMany<Role>()
            .WithMany()
            .UsingEntity(joinBuilder =>
            {
                joinBuilder.ToTable("role_permissions");
                
                //TODO: fill out
                joinBuilder.HasData(
                    // Operator permissions
                    CreateRolePermission(Role.Operator, Permission.GetUser),
                    CreateRolePermission(Role.Operator, Permission.ModifyUser),
                    CreateRolePermission(Role.Operator, Permission.GetCampaign),
                    CreateRolePermission(Role.Operator, Permission.CreateCampaign),
                    CreateRolePermission(Role.Operator, Permission.ModifyCampaign),
                    CreateRolePermission(Role.Operator, Permission.GetReward),
                    CreateRolePermission(Role.Operator, Permission.CreateReward),
                    CreateRolePermission(Role.Operator, Permission.ModifyReward),
                    
                    // Admin permissions
                    CreateRolePermission(Role.Administrator, Permission.GetUser),
                    CreateRolePermission(Role.Administrator, Permission.ModifyUser),
                    CreateRolePermission(Role.Administrator, Permission.GetCampaign),
                    CreateRolePermission(Role.Administrator, Permission.CreateCampaign),
                    CreateRolePermission(Role.Administrator, Permission.ModifyCampaign),
                    CreateRolePermission(Role.Administrator, Permission.GetReward),
                    CreateRolePermission(Role.Administrator, Permission.CreateReward),
                    CreateRolePermission(Role.Administrator, Permission.ModifyReward)
                    );
            });
    }

    private static object CreateRolePermission(Role role, Permission permission)
    {
        return new
        {
            RoleName = role.Name,
            PermissionCode = permission.Code
        };
    }
}
