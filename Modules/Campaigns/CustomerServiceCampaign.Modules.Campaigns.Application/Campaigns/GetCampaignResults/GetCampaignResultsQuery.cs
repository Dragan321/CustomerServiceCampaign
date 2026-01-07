using CustomerServiceCampaign.Common.Application.Messaging;

namespace CustomerServiceCampaign.Modules.Campaigns.Application.Campaigns.GetCampaignResults;

public sealed record GetCampaignResultsQuery(Guid CampaignId) : IQuery<CampaignResultsResponse>;

public sealed record CampaignResultsResponse(
    Guid CampaignId,
    int TotalRewards,
    int SuccessfulPurchases,
    decimal ConversionRate,
    List<RewardResultResponse> Rewards);

public sealed record RewardResultResponse(
    Guid RewardId,
    int CustomerId,
    decimal Discount,
    bool WasPurchaseSuccessful,
    DateTime? PurchaseDate);
