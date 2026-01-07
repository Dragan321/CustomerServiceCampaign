using CustomerServiceCampaign.Common.Domain;

namespace CustomerServiceCampaign.Modules.Campaigns.Domain.Rewards;

public sealed class RewardCancelledDomainEvent(Guid rewardId) : DomainEvent
{
    public Guid RewardId { get; } = rewardId;
}
