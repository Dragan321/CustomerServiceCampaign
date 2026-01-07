using CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns;

namespace CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns.Repositories;

public interface ICampaignRepository
{
    Task<Campaign?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<(IReadOnlyCollection<Campaign> Items, int TotalCount)> GetAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<List<Campaign>> GetActiveExpiredCampaignsAsync(DateTime utcNow, CancellationToken cancellationToken = default);
    Task<List<Campaign>> GetDraftStartingCampaignsAsync(DateTime utcNow, CancellationToken cancellationToken = default);
    void Insert(Campaign campaign);
    void InsertImport(PurchaseImport purchaseImport);
    Task<PurchaseImport?> GetImportByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PurchaseImport?> GetNextPendingImportAsync(CancellationToken cancellationToken = default);
}
