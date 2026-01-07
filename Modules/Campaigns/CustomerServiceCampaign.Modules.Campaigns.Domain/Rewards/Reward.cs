using CustomerServiceCampaign.Common.Domain;

namespace CustomerServiceCampaign.Modules.Campaigns.Domain.Rewards;

public sealed class Reward : Entity
{
    private Reward()
    {
    }

    public Guid Id { get; private set; }

    public Guid CampaignId { get; private set; }

    public int CustomerId { get; private set; }

    public Guid AgentId { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public RewardStatus Status { get; private set; }

    public decimal Discount { get; private set; }

    public bool WasPurchaseSuccessful { get; private set; }

    public DateTime? PurchaseDate { get; private set; }

    public static Reward Create(Guid campaignId, int customerId, Guid agentId, DateTime createdAt, decimal discount)
    {
        var reward = new Reward
        {
            Id = Guid.NewGuid(),
            CampaignId = campaignId,
            CustomerId = customerId,
            AgentId = agentId,
            CreatedAt = createdAt,
            Status = RewardStatus.Active,
            Discount = discount
        };

        reward.Raise(new RewardCreatedDomainEvent(reward.Id));

        return reward;
    }

    public void Cancel()
    {
        Status = RewardStatus.Cancelled;

        Raise(new RewardCancelledDomainEvent(Id));
    }

    public void MarkAsConverted(DateTime purchaseDate)
    {
        WasPurchaseSuccessful = true;
        PurchaseDate = purchaseDate;
    }
}

public enum RewardStatus
{
    Active = 1,
    Cancelled = 2
}
