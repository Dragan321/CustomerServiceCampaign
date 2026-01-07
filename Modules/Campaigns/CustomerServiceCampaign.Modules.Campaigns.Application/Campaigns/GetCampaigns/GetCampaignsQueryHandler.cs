using CustomerServiceCampaign.Common.Application.Messaging;
using CustomerServiceCampaign.Common.Domain;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns.Repositories;

namespace CustomerServiceCampaign.Modules.Campaigns.Application.Campaigns.GetCampaigns;

internal sealed class GetCampaignsQueryHandler(ICampaignRepository campaignRepository)
    : IQueryHandler<GetCampaignsQuery, PagedList<CampaignResponse>>
{
    public async Task<Result<PagedList<CampaignResponse>>> Handle(GetCampaignsQuery request, CancellationToken cancellationToken)
    {
        (IReadOnlyCollection<Campaign> campaigns, int totalCount) = await campaignRepository.GetAsync(
            request.Page,
            request.PageSize,
            cancellationToken);

        var campaignResponses = campaigns.Select(c => new CampaignResponse(
            c.Id,
            c.Name,
            c.StartDate,
            c.EndDate,
            c.Status.ToString())).ToList();

        return PagedList<CampaignResponse>.Create(campaignResponses, request.Page, request.PageSize, totalCount);
    }
}
