using CustomerServiceCampaign.Common.Application.Messaging;
using CustomerServiceCampaign.Common.Domain;
using CustomerServiceCampaign.Modules.Campaigns.Application.Abstractions.Data;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Rewards;

namespace CustomerServiceCampaign.Modules.Campaigns.Application.Rewards.CancelReward;

internal sealed class CancelRewardCommandHandler(
    IRewardRepository rewardRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CancelRewardCommand>
{
    public async Task<Result> Handle(CancelRewardCommand request, CancellationToken cancellationToken)
    {
        Reward? reward = await rewardRepository.GetByIdAsync(request.RewardId, cancellationToken);

        if (reward is null)
        {
            return Result.Failure(RewardErrors.NotFound(request.RewardId));
        }

        if (reward.AgentId != request.UserId)
        {
            return Result.Failure(Error.Forbidden("Rewards.Forbidden", "You are not the issuer of this reward."));
        }

        reward.Cancel();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
