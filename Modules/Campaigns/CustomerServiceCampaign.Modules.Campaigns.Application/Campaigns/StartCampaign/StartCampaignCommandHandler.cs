using CustomerServiceCampaign.Common.Application.Messaging;
using CustomerServiceCampaign.Common.Domain;
using CustomerServiceCampaign.Modules.Campaigns.Application.Abstractions.Data;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns.Repositories;

namespace CustomerServiceCampaign.Modules.Campaigns.Application.Campaigns.StartCampaign;

internal sealed class StartCampaignCommandHandler(
    ICampaignRepository campaignRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<StartCampaignCommand>
{
    public async Task<Result> Handle(StartCampaignCommand request, CancellationToken cancellationToken)
    {
        Campaign? campaign = await campaignRepository.GetByIdAsync(request.CampaignId, cancellationToken);

        if (campaign is null)
        {
            return Result.Failure(CampaignErrors.NotFound(request.CampaignId));
        }

        Result result = campaign.Start();

        if (result.IsFailure)
        {
            return result;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}