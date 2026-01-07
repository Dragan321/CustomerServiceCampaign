using CustomerServiceCampaign.Common.Domain;

namespace CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns;

public static class CampaignErrors
{
    public static Error NotFound(Guid campaignId) => Error.NotFound(
        "Campaigns.NotFound",
        $"The campaign with the identifier {campaignId} was not found.");
    
    public static readonly Error NotActive = Error.Problem(
        "Campaigns.NotActive",
        "The campaign is not currently active.");

    public static readonly Error InvalidStatusTransition = Error.Problem(
        "Campaigns.InvalidStatusTransition",
        "The campaign status transition is invalid.");
}
