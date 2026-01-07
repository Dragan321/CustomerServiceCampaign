using FluentValidation;

namespace CustomerServiceCampaign.Modules.Campaigns.Application.Campaigns.StartCampaign;

internal sealed class StartCampaignCommandValidator : AbstractValidator<StartCampaignCommand>
{
    public StartCampaignCommandValidator()
    {
        RuleFor(c => c.CampaignId).NotEmpty();
    }
}