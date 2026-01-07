using CustomerServiceCampaign.Common.Application.Messaging;

namespace CustomerServiceCampaign.Modules.Campaigns.Application.Campaigns.CreateCampaign;

public sealed record CreateCampaignCommand(
    string Name,
    DateTime StartDate,
    int LengthInDays) : ICommand<Guid>;
