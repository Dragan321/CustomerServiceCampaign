using CustomerServiceCampaign.Modules.Campaigns.Application.Abstractions.Data;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Rewards;
using CustomerServiceCampaign.Modules.Campaigns.Infrastructure.Campaigns;
using CustomerServiceCampaign.Modules.Campaigns.Infrastructure.Rewards;
using Microsoft.EntityFrameworkCore;

namespace CustomerServiceCampaign.Modules.Campaigns.Infrastructure.Database;

public sealed class CampaignsDbContext(DbContextOptions<CampaignsDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<Campaign> Campaigns { get; set; }
    internal DbSet<Reward> Rewards { get; set; }
    internal DbSet<PurchaseImport> PurchaseImports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Campaigns);

        modelBuilder.ApplyConfiguration(new CampaignConfiguration());
        modelBuilder.ApplyConfiguration(new RewardConfiguration());
        modelBuilder.ApplyConfiguration(new PurchaseImportConfiguration());
    }
}
