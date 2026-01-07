using FluentValidation;

namespace CustomerServiceCampaign.Modules.Campaigns.Application.Rewards.CreateReward;

internal sealed class CreateRewardCommandValidator : AbstractValidator<CreateRewardCommand>
{
    public CreateRewardCommandValidator()
    {
        RuleFor(r => r.CampaignId).NotEmpty();
        RuleFor(r => r.CustomerId).GreaterThan(0);
        RuleFor(r => r.Discount).GreaterThan(0);
    }
}
