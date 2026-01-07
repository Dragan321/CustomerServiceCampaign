using CustomerServiceCampaign.Common.Application.Messaging;

namespace CustomerServiceCampaign.Modules.Campaigns.Application.Campaigns.CancelCampaign;

public sealed record CancelCampaignCommand(Guid CampaignId) : ICommand;