using CustomerServiceCampaign.Common.Application.Messaging;

namespace CustomerServiceCampaign.Modules.Campaigns.Application.Rewards.GetRewards;

public sealed record GetRewardsQuery(int Page, int PageSize) : IQuery<PagedList<RewardResponse>>;
