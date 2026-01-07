using CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerServiceCampaign.Modules.Campaigns.Infrastructure.Campaigns;

internal sealed class PurchaseImportConfiguration : IEntityTypeConfiguration<PurchaseImport>
{
    public void Configure(EntityTypeBuilder<PurchaseImport> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.CampaignId).IsRequired();

        builder.Property(p => p.Status).HasConversion<int>();

        builder.Property(p => p.BlobName).HasMaxLength(500);

        builder.Property(p => p.UploadedAt).IsRequired();
    }
}
