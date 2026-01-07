using CustomerServiceCampaign.Common.Domain;

namespace CustomerServiceCampaign.Modules.Campaigns.Domain.Rewards;

public static class RewardErrors
{
    public static readonly Error DailyLimitExceeded = Error.Problem(
        "Rewards.DailyLimitExceeded",
        "The agent has already rewarded 5 customers today.");

    public static readonly Error AlreadyRewarded = Error.Conflict(
        "Rewards.AlreadyRewarded",
        "The customer has already been rewarded for this campaign.");
    
    public static Error NotFound(Guid rewardId) => Error.NotFound(
        "Rewards.NotFound",
        $"The reward with the identifier {rewardId} was not found.");

    public static readonly Error InvalidCustomer = Error.Problem(
        "Rewards.InvalidCustomer",
        "The customer is not valid for this campaign.");
}
