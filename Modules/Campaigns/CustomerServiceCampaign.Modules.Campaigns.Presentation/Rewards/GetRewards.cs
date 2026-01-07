using CustomerServiceCampaign.Common.Domain;
using CustomerServiceCampaign.Common.Presentation.Endpoints;
using CustomerServiceCampaign.Common.Presentation.Results;
using CustomerServiceCampaign.Modules.Campaigns.Application.Rewards.GetRewards;
using CustomerServiceCampaign.Common.Application.Messaging;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CustomerServiceCampaign.Modules.Campaigns.Presentation.Rewards;

internal sealed class GetRewards : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("rewards", async (int page, int pageSize, ISender sender) =>
        {
            Result<PagedList<RewardResponse>> result = await sender.Send(new GetRewardsQuery(page, pageSize));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization("rewards:read")
        .WithTags(Tags.Rewards);
    }
}
