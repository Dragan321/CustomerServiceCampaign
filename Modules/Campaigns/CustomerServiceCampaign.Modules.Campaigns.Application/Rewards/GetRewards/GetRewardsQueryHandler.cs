using CustomerServiceCampaign.Common.Application.Messaging;
using CustomerServiceCampaign.Common.Domain;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Rewards;

namespace CustomerServiceCampaign.Modules.Campaigns.Application.Rewards.GetRewards;

internal sealed class GetRewardsQueryHandler(IRewardRepository rewardRepository)
    : IQueryHandler<GetRewardsQuery, PagedList<RewardResponse>>
{
    public async Task<Result<PagedList<RewardResponse>>> Handle(GetRewardsQuery request, CancellationToken cancellationToken)
    {
        (IReadOnlyCollection<Reward> rewards, int totalCount) = await rewardRepository.GetAsync(
            request.Page,
            request.PageSize,
            cancellationToken);

        var rewardResponses = rewards.Select(r => new RewardResponse(
            r.Id,
            r.CampaignId,
            r.CustomerId,
            r.AgentId,
            r.CreatedAt,
            r.Status.ToString(),
            r.Discount)).ToList();

        return PagedList<RewardResponse>.Create(rewardResponses, request.Page, request.PageSize, totalCount);
    }
}
