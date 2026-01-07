using CustomerServiceCampaign.Common.Application.Messaging;

namespace CustomerServiceCampaign.Modules.Campaigns.Application.Campaigns.UploadPurchases;

public sealed record UploadPurchasesCommand(Guid CampaignId, Stream FileStream, string ContentType) : ICommand<Guid>;
