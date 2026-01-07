using CustomerServiceCampaign.Common.Application.Messaging;

namespace CustomerServiceCampaign.Modules.Campaigns.Application.Rewards.CancelReward;

public sealed record CancelRewardCommand(Guid RewardId, Guid UserId) : ICommand;
