using CustomerServiceCampaign.Common.Application.Clock;
using CustomerServiceCampaign.Common.Application.Messaging;
using CustomerServiceCampaign.Common.Domain;
using CustomerServiceCampaign.Modules.Campaigns.Application.Abstractions.Data;
using CustomerServiceCampaign.Modules.Campaigns.Application.Abstractions.Storage;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns.Repositories;

namespace CustomerServiceCampaign.Modules.Campaigns.Application.Campaigns.UploadPurchases;

internal sealed class UploadPurchasesCommandHandler(
    ICampaignRepository campaignRepository,
    IBlobService blobService,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<UploadPurchasesCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UploadPurchasesCommand request, CancellationToken cancellationToken)
    {
        Campaign? campaign = await campaignRepository.GetByIdAsync(request.CampaignId, cancellationToken);

        if (campaign is null)
        {
            return Result.Failure<Guid>(CampaignErrors.NotFound(request.CampaignId));
        }

        string blobName = await blobService.UploadAsync(request.FileStream, request.ContentType, cancellationToken);

        var purchaseImport = PurchaseImport.Create(
            request.CampaignId,
            blobName,
            dateTimeProvider.UtcNow);

        campaignRepository.InsertImport(purchaseImport);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        // TODO: Trigger background job to process the import

        return purchaseImport.Id;
    }
}
