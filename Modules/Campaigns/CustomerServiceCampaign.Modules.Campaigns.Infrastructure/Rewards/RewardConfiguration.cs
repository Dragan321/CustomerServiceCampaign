using CustomerServiceCampaign.Modules.Campaigns.Domain.Rewards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerServiceCampaign.Modules.Campaigns.Infrastructure.Rewards;

internal sealed class RewardConfiguration : IEntityTypeConfiguration<Reward>
{
    public void Configure(EntityTypeBuilder<Reward> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.CampaignId).IsRequired();

        builder.Property(r => r.CustomerId).IsRequired();

        builder.Property(r => r.AgentId).IsRequired();

        builder.Property(r => r.CreatedAt).IsRequired();

        builder.Property(r => r.Discount).HasPrecision(18, 2);

        builder.Property(r => r.WasPurchaseSuccessful).IsRequired();

        builder.Property(r => r.PurchaseDate);

        builder.Property(r => r.Status).HasConversion<int>();
        
        builder.HasIndex(r => new { r.CampaignId, r.CustomerId }).IsUnique();
        
        builder.HasIndex(r => new { r.AgentId, r.CreatedAt });
    }
}
