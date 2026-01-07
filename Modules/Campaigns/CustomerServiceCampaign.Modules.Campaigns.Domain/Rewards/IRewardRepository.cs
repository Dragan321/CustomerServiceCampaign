using CustomerServiceCampaign.Modules.Campaigns.Domain.Rewards;

namespace CustomerServiceCampaign.Modules.Campaigns.Domain.Rewards;

public interface IRewardRepository
{
    Task<Reward?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<(IReadOnlyCollection<Reward> Items, int TotalCount)> GetAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    void Insert(Reward reward);
    Task<List<Reward>> GetByCampaignIdAsync(Guid campaignId, CancellationToken cancellationToken = default);
    Task<int> GetCountByAgentAndDateAsync(Guid agentId, DateTime date, CancellationToken cancellationToken = default);
    Task<bool> HasCustomerBeenRewardedAsync(Guid campaignId, int customerId, CancellationToken cancellationToken = default);
}
