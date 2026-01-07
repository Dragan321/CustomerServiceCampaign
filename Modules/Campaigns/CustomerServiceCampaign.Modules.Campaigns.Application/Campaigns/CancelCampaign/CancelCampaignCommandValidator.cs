using FluentValidation;

namespace CustomerServiceCampaign.Modules.Campaigns.Application.Campaigns.CancelCampaign;

internal sealed class CancelCampaignCommandValidator : AbstractValidator<CancelCampaignCommand>
{
    public CancelCampaignCommandValidator()
    {
        RuleFor(c => c.CampaignId).NotEmpty();
    }
}