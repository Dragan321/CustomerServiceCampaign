using CustomerServiceCampaign.Common.Application.Messaging;
using CustomerServiceCampaign.Common.Domain;
using CustomerServiceCampaign.Modules.Campaigns.Application.Abstractions.Data;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns.Repositories;

namespace CustomerServiceCampaign.Modules.Campaigns.Application.Campaigns.CreateCampaign;

internal sealed class CreateCampaignCommandHandler(
    ICampaignRepository campaignRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCampaignCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
    {
        var campaign = Campaign.Create(
            request.Name,
            request.StartDate,
            request.LengthInDays);

        campaignRepository.Insert(campaign);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return campaign.Id;
    }
}
