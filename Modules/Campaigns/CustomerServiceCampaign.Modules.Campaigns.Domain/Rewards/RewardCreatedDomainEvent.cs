using CustomerServiceCampaign.Common.Domain;

namespace CustomerServiceCampaign.Modules.Campaigns.Domain.Rewards;

public sealed class RewardCreatedDomainEvent(Guid rewardId) : DomainEvent
{
    public Guid RewardId { get; } = rewardId;
}
