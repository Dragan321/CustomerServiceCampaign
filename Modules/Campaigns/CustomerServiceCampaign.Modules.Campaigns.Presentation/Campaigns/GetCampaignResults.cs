using CustomerServiceCampaign.Common.Domain;
using CustomerServiceCampaign.Common.Presentation.Endpoints;
using CustomerServiceCampaign.Common.Presentation.Results;
using CustomerServiceCampaign.Modules.Campaigns.Application.Campaigns.GetCampaignResults;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CustomerServiceCampaign.Modules.Campaigns.Presentation.Campaigns;

internal sealed class GetCampaignResults : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("campaigns/{id:guid}/results", async (Guid id, ISender sender) =>
        {
            Result<CampaignResultsResponse> result = await sender.Send(new GetCampaignResultsQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization("campaigns:read")
        .WithTags(Tags.Campaigns);
    }
}
