using CustomerServiceCampaign.Common.Application.Messaging;

namespace CustomerServiceCampaign.Modules.Campaigns.Application.Campaigns.StartCampaign;

public sealed record StartCampaignCommand(Guid CampaignId) : ICommand;