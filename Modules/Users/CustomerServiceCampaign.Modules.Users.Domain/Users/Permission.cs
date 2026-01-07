namespace CustomerServiceCampaign.Modules.Users.Domain.Users;

public sealed class Permission
{
    public static readonly Permission GetUser = new("users:read");
    public static readonly Permission ModifyUser = new("users:update");

    public static readonly Permission GetCampaign = new("campaigns:read");
    public static readonly Permission CreateCampaign = new("campaigns:create");
    public static readonly Permission ModifyCampaign = new("campaigns:update");
    public static readonly Permission GetReward = new("rewards:read");
    public static readonly Permission CreateReward = new("rewards:create");
    public static readonly Permission ModifyReward = new("rewards:update");

    public Permission(string code)
    {
        Code = code;
    }

    public string Code { get; }
}
