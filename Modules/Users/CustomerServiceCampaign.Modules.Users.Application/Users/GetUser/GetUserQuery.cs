using CustomerServiceCampaign.Common.Application.Messaging;

namespace CustomerServiceCampaign.Modules.Users.Application.Users.GetUser;

public sealed record GetUserQuery(Guid UserId) : IQuery<UserResponse>;
