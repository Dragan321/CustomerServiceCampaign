using CustomerServiceCampaign.Common.Application.Authorization;
using CustomerServiceCampaign.Common.Application.Messaging;

namespace CustomerServiceCampaign.Modules.Users.Application.Users.GetUserPermissions;

public sealed record GetUserPermissionsQuery(string IdentityId) : IQuery<PermissionsResponse>;
