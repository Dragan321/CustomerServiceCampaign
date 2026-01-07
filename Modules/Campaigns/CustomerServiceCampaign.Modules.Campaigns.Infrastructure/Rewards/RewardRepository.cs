using CustomerServiceCampaign.Modules.Campaigns.Domain.Rewards;
using CustomerServiceCampaign.Modules.Campaigns.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CustomerServiceCampaign.Modules.Campaigns.Infrastructure.Rewards;

internal sealed class RewardRepository(CampaignsDbContext context) : IRewardRepository
{
    public async Task<Reward?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Rewards.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<(IReadOnlyCollection<Reward> Items, int TotalCount)> GetAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        int totalCount = await context.Rewards.CountAsync(cancellationToken);

        List<Reward> items = await context.Rewards
            .OrderByDescending(r => r.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public void Insert(Reward reward)
    {
        context.Rewards.Add(reward);
    }

    public async Task<List<Reward>> GetByCampaignIdAsync(Guid campaignId, CancellationToken cancellationToken = default)
    {
        return await context.Rewards
            .Where(r => r.CampaignId == campaignId)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetCountByAgentAndDateAsync(Guid agentId, DateTime date, CancellationToken cancellationToken = default)
    {
        return await context.Rewards
            .CountAsync(r => r.AgentId == agentId && r.CreatedAt.Date == date.Date && r.Status == RewardStatus.Active, cancellationToken);
    }

    public async Task<bool> HasCustomerBeenRewardedAsync(Guid campaignId, int customerId, CancellationToken cancellationToken = default)
    {
        return await context.Rewards
            .AnyAsync(r => r.CampaignId == campaignId && r.CustomerId == customerId && r.Status == RewardStatus.Active, cancellationToken);
    }
}
