using CustomerServiceCampaign.Common.Domain;
using CustomerServiceCampaign.Common.Presentation.Endpoints;
using CustomerServiceCampaign.Common.Presentation.Results;
using CustomerServiceCampaign.Modules.Campaigns.Application.Campaigns.StartCampaign;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CustomerServiceCampaign.Modules.Campaigns.Presentation.Campaigns;

internal sealed class StartCampaign : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("campaigns/{id:guid}/start", async (Guid id, ISender sender) =>
        {
            Result result = await sender.Send(new StartCampaignCommand(id));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .RequireAuthorization("campaigns:update")
        .WithTags(Tags.Campaigns);
    }
}
