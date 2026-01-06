using CustomerServiceCampaign.Common.Application.Authorization;
using CustomerServiceCampaign.Common.Domain;
using CustomerServiceCampaign.Modules.Users.Application.Users.GetUserPermissions;
using MediatR;

namespace CustomerServiceCampaign.Modules.Users.Infrastructure.Authorization;

internal sealed class PermissionService(ISender sender) : IPermissionService
{
    public async Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId)
    {
        return await sender.Send(new GetUserPermissionsQuery(identityId));
    }
}
