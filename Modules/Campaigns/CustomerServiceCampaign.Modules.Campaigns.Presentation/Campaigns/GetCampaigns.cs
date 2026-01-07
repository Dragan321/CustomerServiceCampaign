using CustomerServiceCampaign.Common.Domain;
using CustomerServiceCampaign.Common.Presentation.Endpoints;
using CustomerServiceCampaign.Common.Presentation.Results;
using CustomerServiceCampaign.Modules.Campaigns.Application.Campaigns.GetCampaigns;
using CustomerServiceCampaign.Common.Application.Messaging;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CustomerServiceCampaign.Modules.Campaigns.Presentation.Campaigns;

internal sealed class GetCampaigns : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("campaigns", async (int page, int pageSize, ISender sender) =>
        {
            Result<PagedList<CampaignResponse>> result = await sender.Send(new GetCampaignsQuery(page, pageSize));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization("campaigns:read")
        .WithTags(Tags.Campaigns);
    }
}
