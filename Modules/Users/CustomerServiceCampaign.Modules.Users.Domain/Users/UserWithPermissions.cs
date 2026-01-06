namespace CustomerServiceCampaign.Modules.Users.Domain.Users;

public record UserWithPermissions(Guid UserId, HashSet<string> Permission);