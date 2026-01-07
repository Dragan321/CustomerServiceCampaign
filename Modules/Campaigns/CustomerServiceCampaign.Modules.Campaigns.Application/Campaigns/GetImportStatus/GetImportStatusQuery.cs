using CustomerServiceCampaign.Common.Application.Messaging;

namespace CustomerServiceCampaign.Modules.Campaigns.Application.Campaigns.GetImportStatus;

public sealed record GetImportStatusQuery(Guid ImportId) : IQuery<ImportStatusResponse>;

public sealed record ImportStatusResponse(
    Guid ImportId,
    Guid CampaignId,
    string Status,
    DateTime UploadedAt);
