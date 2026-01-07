namespace CustomerServiceCampaign.Modules.Campaigns.Application.Rewards.GetRewards;

public sealed record RewardResponse(
    Guid Id,
    Guid CampaignId,
    int CustomerId,
    Guid AgentId,
    DateTime CreatedAt,
    string Status,
    decimal Discount);
