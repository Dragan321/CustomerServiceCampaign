using FluentValidation;

namespace CustomerServiceCampaign.Modules.Campaigns.Application.Rewards.CancelReward;

internal sealed class CancelRewardCommandValidator : AbstractValidator<CancelRewardCommand>
{
    public CancelRewardCommandValidator()
    {
        RuleFor(c => c.RewardId).NotEmpty();
    }
}
