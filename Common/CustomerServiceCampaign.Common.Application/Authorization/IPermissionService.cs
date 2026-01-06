using CustomerServiceCampaign.Common.Domain;

namespace CustomerServiceCampaign.Common.Application.Authorization;

public interface IPermissionService
{
    Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId);
}
