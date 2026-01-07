using CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns.Repositories;
using CustomerServiceCampaign.Modules.Campaigns.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CustomerServiceCampaign.Modules.Campaigns.Infrastructure.Campaigns;

internal sealed class CampaignRepository(CampaignsDbContext context) : ICampaignRepository
{
    public async Task<Campaign?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Campaigns.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<(IReadOnlyCollection<Campaign> Items, int TotalCount)> GetAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        int totalCount = await context.Campaigns.CountAsync(cancellationToken);

        List<Campaign> items = await context.Campaigns
            .OrderByDescending(c => c.StartDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public async Task<List<Campaign>> GetActiveExpiredCampaignsAsync(DateTime utcNow, CancellationToken cancellationToken = default)
    {
        return await context.Campaigns
            .Where(c => c.Status == CampaignStatus.Active && c.EndDate <= utcNow)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Campaign>> GetDraftStartingCampaignsAsync(DateTime utcNow, CancellationToken cancellationToken = default)
    {
        return await context.Campaigns
            .Where(c => c.Status == CampaignStatus.Draft && c.StartDate <= utcNow)
            .ToListAsync(cancellationToken);
    }

    public void Insert(Campaign campaign)
    {
        context.Campaigns.Add(campaign);
    }

    public void InsertImport(PurchaseImport purchaseImport)
    {
        context.PurchaseImports.Add(purchaseImport);
    }

    public async Task<PurchaseImport?> GetImportByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.PurchaseImports.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<PurchaseImport?> GetNextPendingImportAsync(CancellationToken cancellationToken = default)
    {
        return await context.PurchaseImports
            .Where(p => p.Status == PurchaseImportStatus.Pending)
            .OrderBy(p => p.UploadedAt)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
