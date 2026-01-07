using CustomerServiceCampaign.Common.Domain;
using CustomerServiceCampaign.Common.Presentation.Endpoints;
using CustomerServiceCampaign.Common.Presentation.Results;
using CustomerServiceCampaign.Modules.Campaigns.Application.Campaigns.CreateCampaign;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CustomerServiceCampaign.Modules.Campaigns.Presentation.Campaigns;

internal sealed class CreateCampaign : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("campaigns", async (Request request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreateCampaignCommand(
                request.Name,
                request.StartDate,
                request.LengthInDays));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization("campaigns:create")
        .WithTags(Tags.Campaigns);
    }

    internal sealed class Request
    {
        public string Name { get; init; }

        public DateTime StartDate { get; init; }

        public int LengthInDays { get; init; } = 5;
    }
}
