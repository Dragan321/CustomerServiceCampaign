using CustomerServiceCampaign.Common.Application.Messaging;

namespace CustomerServiceCampaign.Modules.Users.Application.Users.UpdateUser;

public sealed record UpdateUserCommand(Guid UserId, string FirstName, string LastName) : ICommand;
