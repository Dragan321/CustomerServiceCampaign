using System.Security.Claims;
using CustomerServiceCampaign.Common.Domain;
using CustomerServiceCampaign.Common.Infrastructure.Authentication;
using CustomerServiceCampaign.Common.Presentation.Endpoints;
using CustomerServiceCampaign.Common.Presentation.Results;
using CustomerServiceCampaign.Modules.Campaigns.Application.Rewards.CancelReward;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CustomerServiceCampaign.Modules.Campaigns.Presentation.Rewards;

internal sealed class CancelReward : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("rewards/{id:guid}/cancel", async (Guid id, ClaimsPrincipal claimsPrincipal, ISender sender) =>
        {
            Result result = await sender.Send(new CancelRewardCommand(id, claimsPrincipal.GetUserId()));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .RequireAuthorization("rewards:update")
        .WithTags(Tags.Rewards);
    }
}
