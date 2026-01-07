using FluentValidation;

namespace CustomerServiceCampaign.Modules.Campaigns.Application.Campaigns.CreateCampaign;

internal sealed class CreateCampaignCommandValidator : AbstractValidator<CreateCampaignCommand>
{
    public CreateCampaignCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.StartDate).NotEmpty().GreaterThan(DateTime.Now);
        RuleFor(c => c.LengthInDays).GreaterThan(0);
    }
}
