namespace CustomerServiceCampaign.Modules.Campaigns.Application.Campaigns.GetCampaigns;

public sealed record CampaignResponse(
    Guid Id,
    string Name,
    DateTime StartDate,
    DateTime EndDate,
    string Status);
