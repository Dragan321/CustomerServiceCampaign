using CustomerServiceCampaign.Common.Application.Messaging;
using CustomerServiceCampaign.Common.Domain;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns.Repositories;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Rewards;

namespace CustomerServiceCampaign.Modules.Campaigns.Application.Campaigns.GetCampaignResults;

internal sealed class GetCampaignResultsQueryHandler(
    ICampaignRepository campaignRepository,
    IRewardRepository rewardRepository)
    : IQueryHandler<GetCampaignResultsQuery, CampaignResultsResponse>
{
    public async Task<Result<CampaignResultsResponse>> Handle(GetCampaignResultsQuery request, CancellationToken cancellationToken)
    {
        Campaign? campaign = await campaignRepository.GetByIdAsync(request.CampaignId, cancellationToken);

        if (campaign is null)
        {
            return Result.Failure<CampaignResultsResponse>(CampaignErrors.NotFound(request.CampaignId));
        }

        List<Reward> rewards = await rewardRepository.GetByCampaignIdAsync(request.CampaignId, cancellationToken);

        int totalRewards = rewards.Count;
        int successfulPurchases = rewards.Count(r => r.WasPurchaseSuccessful);
        decimal conversionRate = totalRewards > 0 ? (decimal)successfulPurchases / totalRewards * 100 : 0;

        var response = new CampaignResultsResponse(
            campaign.Id,
            totalRewards,
            successfulPurchases,
            conversionRate,
            rewards.Select(r => new RewardResultResponse(
                r.Id,
                r.CustomerId,
                r.Discount,
                r.WasPurchaseSuccessful,
                r.PurchaseDate)).ToList());

        return response;
    }
}
