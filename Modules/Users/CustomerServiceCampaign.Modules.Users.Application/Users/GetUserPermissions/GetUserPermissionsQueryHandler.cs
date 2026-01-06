using CustomerServiceCampaign.Common.Application.Authorization;
using CustomerServiceCampaign.Common.Application.Messaging;
using CustomerServiceCampaign.Common.Domain;
using CustomerServiceCampaign.Modules.Users.Domain.Users;

namespace CustomerServiceCampaign.Modules.Users.Application.Users.GetUserPermissions;

internal sealed class GetUserPermissionsQueryHandler(IUserRepository userRepository)
    : IQueryHandler<GetUserPermissionsQuery, PermissionsResponse>
{
    public async Task<Result<PermissionsResponse>> Handle(
        GetUserPermissionsQuery request,
        CancellationToken cancellationToken)
    {
        var userPermissions = await userRepository.GetUserPermissions(request.IdentityId, cancellationToken);

        if (userPermissions is null)
        {
            return Result.Failure<PermissionsResponse>(UserErrors.NotFound(request.IdentityId));
        }

        if (userPermissions.Permission.Count == 0)
        {
            return Result.Failure<PermissionsResponse>(UserErrors.NotFound(request.IdentityId));
        }

        return new PermissionsResponse(userPermissions.UserId, userPermissions.Permission );
    }
}
