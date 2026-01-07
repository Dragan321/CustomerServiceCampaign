using System.Security.Claims;
using CustomerServiceCampaign.Common.Domain;
using CustomerServiceCampaign.Common.Infrastructure.Authentication;
using CustomerServiceCampaign.Common.Presentation.Endpoints;
using CustomerServiceCampaign.Common.Presentation.Results;
using CustomerServiceCampaign.Modules.Campaigns.Application.Rewards.CreateReward;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CustomerServiceCampaign.Modules.Campaigns.Presentation.Rewards;

internal sealed class CreateReward : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("rewards", async (Request request, ClaimsPrincipal claimsPrincipal, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreateRewardCommand(
                request.CampaignId,
                request.CustomerId,
                claimsPrincipal.GetUserId(),
                request.Discount));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization("rewards:create")
        .WithTags(Tags.Rewards);
    }

    internal sealed class Request
    {
        public Guid CampaignId { get; init; }

        public int CustomerId { get; init; }

        public decimal Discount { get; init; }
    }
}
