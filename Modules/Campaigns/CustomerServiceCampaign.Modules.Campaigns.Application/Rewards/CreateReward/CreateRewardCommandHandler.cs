using CustomerServiceCampaign.Common.Application.Clock;
using CustomerServiceCampaign.Common.Application.Messaging;
using CustomerServiceCampaign.Common.Domain;
using CustomerServiceCampaign.Modules.Campaigns.Application.Abstractions.Customers;
using CustomerServiceCampaign.Modules.Campaigns.Application.Abstractions.Data;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns.Repositories;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Rewards;

namespace CustomerServiceCampaign.Modules.Campaigns.Application.Rewards.CreateReward;

internal sealed class CreateRewardCommandHandler(
    ICampaignRepository campaignRepository,
    IRewardRepository rewardRepository,
    ICustomerService customerService,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<CreateRewardCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateRewardCommand request, CancellationToken cancellationToken)
    {
        Campaign? campaign = await campaignRepository.GetByIdAsync(request.CampaignId, cancellationToken);

        if (campaign is null)
        {
            return Result.Failure<Guid>(CampaignErrors.NotFound(request.CampaignId));
        }

        if (campaign.Status != CampaignStatus.Active)
        {
            return Result.Failure<Guid>(CampaignErrors.NotActive);
        }

        bool isCustomerValid = await customerService.IsValidAsync(request.CustomerId, cancellationToken);

        if (!isCustomerValid)
        {
            return Result.Failure<Guid>(RewardErrors.InvalidCustomer);
        }
        
        int rewardsIssuedToday = await rewardRepository.GetCountByAgentAndDateAsync(
            request.UserId,
            dateTimeProvider.UtcNow.Date,
            cancellationToken);

        if (rewardsIssuedToday >= 5)
        {
            return Result.Failure<Guid>(RewardErrors.DailyLimitExceeded);
        }

        bool alreadyRewarded = await rewardRepository.HasCustomerBeenRewardedAsync(
            request.CampaignId,
            request.CustomerId,
            cancellationToken);

        if (alreadyRewarded)
        {
            return Result.Failure<Guid>(RewardErrors.AlreadyRewarded);
        }

        var reward = Reward.Create(
            request.CampaignId,
            request.CustomerId,
            request.UserId,
            dateTimeProvider.UtcNow,
            request.Discount);

        rewardRepository.Insert(reward);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return reward.Id;
    }
}
