using CustomerServiceCampaign.Common.Application.Messaging;
using CustomerServiceCampaign.Common.Domain;
using CustomerServiceCampaign.Modules.Users.Domain.Users;

namespace CustomerServiceCampaign.Modules.Users.Application.Users.GetUser;

internal sealed class GetUserQueryHandler(IUserRepository userRepository)
    : IQueryHandler<GetUserQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetAsync(request.UserId, cancellationToken);
        
        if (user is null)
        {
            return Result.Failure<UserResponse>(UserErrors.NotFound(request.UserId));
        }

        return new UserResponse(user.Id, user.Email,user.FirstName, user.LastName);
    }
}
