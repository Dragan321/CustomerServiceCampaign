using CustomerServiceCampaign.Common.Application.Messaging;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Rewards;
using Microsoft.Extensions.Logging;

namespace CustomerServiceCampaign.Modules.Campaigns.Application.Rewards.RewardCancelled;

internal sealed class RewardCancelledDomainEventHandler(ILogger<RewardCancelledDomainEventHandler> logger)
    : DomainEventHandler<RewardCancelledDomainEvent>
{
    public override Task Handle(RewardCancelledDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Reward {RewardId} was cancelled.", domainEvent.RewardId);

        // TODO: Notify the customer about the reward cancellation.
        // This could involve:
        // 1. Fetching the customer email/phone.
        // 2. Sending an email/SMS notifying that the reward is no longer valid.

        return Task.CompletedTask;
    }
}
