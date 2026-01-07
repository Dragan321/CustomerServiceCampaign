using CustomerServiceCampaign.Common.Domain;
using CustomerServiceCampaign.Common.Presentation.Endpoints;
using CustomerServiceCampaign.Common.Presentation.Results;
using CustomerServiceCampaign.Modules.Campaigns.Application.Campaigns.UploadPurchases;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace CustomerServiceCampaign.Modules.Campaigns.Presentation.Campaigns;

internal sealed class UploadPurchases : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("campaigns/{id:guid}/imports", async (
            Guid id,
            [FromForm] IFormFile file,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            if (file.Length == 0)
            {
                return Results.BadRequest("No file uploaded.");
            }

            if (!file.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            {
                return Results.BadRequest("Only CSV files are supported.");
            }

            using Stream stream = file.OpenReadStream();

            Result<Guid> result = await sender.Send(new UploadPurchasesCommand(
                id,
                stream,
                file.ContentType), cancellationToken);

            return result.Match(importId => Results.Accepted($"/api/v1/campaigns/imports/{importId}"), ApiResults.Problem);
        })
        .DisableAntiforgery()
        .RequireAuthorization("campaigns:update")
        .WithTags(Tags.Campaigns);
    }
}
