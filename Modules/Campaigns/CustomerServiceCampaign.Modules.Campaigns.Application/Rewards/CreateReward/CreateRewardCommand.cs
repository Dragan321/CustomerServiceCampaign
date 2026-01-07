using CustomerServiceCampaign.Common.Application.Messaging;

namespace CustomerServiceCampaign.Modules.Campaigns.Application.Rewards.CreateReward;

public sealed record CreateRewardCommand(
    Guid CampaignId,
    int CustomerId,
    Guid UserId,
    decimal Discount) : ICommand<Guid>;
