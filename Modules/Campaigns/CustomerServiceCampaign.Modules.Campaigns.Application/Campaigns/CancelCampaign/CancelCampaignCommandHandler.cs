using CustomerServiceCampaign.Common.Application.Messaging;
using CustomerServiceCampaign.Common.Domain;
using CustomerServiceCampaign.Modules.Campaigns.Application.Abstractions.Data;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns.Repositories;

namespace CustomerServiceCampaign.Modules.Campaigns.Application.Campaigns.CancelCampaign;

internal sealed class CancelCampaignCommandHandler(
    ICampaignRepository campaignRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CancelCampaignCommand>
{
    public async Task<Result> Handle(CancelCampaignCommand request, CancellationToken cancellationToken)
    {
        Campaign? campaign = await campaignRepository.GetByIdAsync(request.CampaignId, cancellationToken);

        if (campaign is null)
        {
            return Result.Failure(CampaignErrors.NotFound(request.CampaignId));
        }

        Result result = campaign.Cancel();

        if (result.IsFailure)
        {
            return result;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}