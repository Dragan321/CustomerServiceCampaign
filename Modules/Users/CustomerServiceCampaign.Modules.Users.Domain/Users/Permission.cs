namespace CustomerServiceCampaign.Modules.Users.Domain.Users;

public sealed class Permission
{
    public static readonly Permission GetUser = new("users:read");
    public static readonly Permission ModifyUser = new("users:update");
    
    //TODO: Add more permissions

    public Permission(string code)
    {
        Code = code;
    }

    public string Code { get; }
}
