using CustomerServiceCampaign.Common.Domain;
using CustomerServiceCampaign.Common.Presentation.Endpoints;
using CustomerServiceCampaign.Common.Presentation.Results;
using CustomerServiceCampaign.Modules.Campaigns.Application.Campaigns.GetImportStatus;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CustomerServiceCampaign.Modules.Campaigns.Presentation.Campaigns;

internal sealed class GetImportStatus : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("campaigns/imports/{id:guid}", async (Guid id, ISender sender) =>
        {
            Result<ImportStatusResponse> result = await sender.Send(new GetImportStatusQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization("campaigns:read")
        .WithTags(Tags.Campaigns);
    }
}
