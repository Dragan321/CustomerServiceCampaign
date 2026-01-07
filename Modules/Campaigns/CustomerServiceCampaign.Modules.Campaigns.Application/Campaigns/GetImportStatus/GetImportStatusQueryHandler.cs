using CustomerServiceCampaign.Common.Application.Messaging;
using CustomerServiceCampaign.Common.Domain;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns.Repositories;

namespace CustomerServiceCampaign.Modules.Campaigns.Application.Campaigns.GetImportStatus;

internal sealed class GetImportStatusQueryHandler(ICampaignRepository campaignRepository)
    : IQueryHandler<GetImportStatusQuery, ImportStatusResponse>
{
    public async Task<Result<ImportStatusResponse>> Handle(GetImportStatusQuery request, CancellationToken cancellationToken)
    {
        PurchaseImport? purchaseImport = await campaignRepository.GetImportByIdAsync(request.ImportId, cancellationToken);

        if (purchaseImport is null)
        {
            return Result.Failure<ImportStatusResponse>(Error.NotFound("Imports.NotFound", "The import was not found."));
        }

        return new ImportStatusResponse(
            purchaseImport.Id,
            purchaseImport.CampaignId,
            purchaseImport.Status.ToString(),
            purchaseImport.UploadedAt);
    }
}
