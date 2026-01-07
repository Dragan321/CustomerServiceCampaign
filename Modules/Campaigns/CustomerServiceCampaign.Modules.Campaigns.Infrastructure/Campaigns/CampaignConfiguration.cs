using CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerServiceCampaign.Modules.Campaigns.Infrastructure.Campaigns;

internal sealed class CampaignConfiguration : IEntityTypeConfiguration<Campaign>
{
    public void Configure(EntityTypeBuilder<Campaign> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).HasMaxLength(200);

        builder.Property(c => c.StartDate).IsRequired();

        builder.Property(c => c.EndDate).IsRequired();
        
        builder.Property(c => c.LengthInDays).IsRequired();

        builder.Property(c => c.Status).HasConversion<int>();
    }
}
