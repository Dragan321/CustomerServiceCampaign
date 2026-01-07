using CustomerServiceCampaign.Common.Application.Messaging;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Rewards;
using Microsoft.Extensions.Logging;

namespace CustomerServiceCampaign.Modules.Campaigns.Application.Rewards.RewardCreated;

internal sealed class RewardCreatedDomainEventHandler(ILogger<RewardCreatedDomainEventHandler> logger)
    : DomainEventHandler<RewardCreatedDomainEvent>
{
    public override Task Handle(RewardCreatedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Reward {RewardId} was created.", domainEvent.RewardId);

        // TODO: Notify the customer about the reward issuance.
        // This could involve:
        // 1. Fetching the customer email/phone from the Users module or an external service.
        // 2. Sending an email/SMS with the discount details.
        // 3. Notifying the 
        return Task.CompletedTask;
    }
}
