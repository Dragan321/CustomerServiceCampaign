using CustomerServiceCampaign.Common.Domain;

namespace CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns;

public sealed class PurchaseImport : Entity
{
    private PurchaseImport()
    {
    }

    public Guid Id { get; private set; }

    public Guid CampaignId { get; private set; }

    public PurchaseImportStatus Status { get; private set; }

    public string? BlobName { get; private set; }

    public DateTime UploadedAt { get; private set; }

    public static PurchaseImport Create(Guid campaignId, string? blobName, DateTime uploadedAt)
    {
        return new PurchaseImport
        {
            Id = Guid.NewGuid(),
            CampaignId = campaignId,
            Status = PurchaseImportStatus.Pending,
            BlobName = blobName,
            UploadedAt = uploadedAt
        };
    }

    public void MarkAsProcessing()
    {
        Status = PurchaseImportStatus.Processing;
    }

    public void MarkAsCompleted()
    {
        Status = PurchaseImportStatus.Completed;
    }

    public void MarkAsFailed()
    {
        Status = PurchaseImportStatus.Failed;
    }
}
