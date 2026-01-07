using CustomerServiceCampaign.Common.Application.Clock;
using CustomerServiceCampaign.Modules.Campaigns.Application.Abstractions.Data;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns.Repositories;
using Microsoft.Extensions.Logging;
using Quartz;

namespace CustomerServiceCampaign.Modules.Campaigns.Infrastructure.Campaigns;

[DisallowConcurrentExecution]
internal sealed class CampaignStatusUpdateJob(
    ICampaignRepository campaignRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider,
    ILogger<CampaignStatusUpdateJob> logger) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Starting campaign status update job");

        DateTime utcNow = dateTimeProvider.UtcNow;

        List<Campaign> campaignsToStart = await campaignRepository.GetDraftStartingCampaignsAsync(
            utcNow,
            context.CancellationToken);

        foreach (Campaign campaign in campaignsToStart)
        {
            logger.LogInformation("Starting campaign {CampaignId}", campaign.Id);
            campaign.Start();
        }

        List<Campaign> campaignsToComplete = await campaignRepository.GetActiveExpiredCampaignsAsync(
            utcNow,
            context.CancellationToken);

        foreach (Campaign campaign in campaignsToComplete)
        {
            logger.LogInformation("Completing campaign {CampaignId}", campaign.Id);
            campaign.Complete();
        }

        if (campaignsToStart.Count > 0 || campaignsToComplete.Count > 0)
        {
            await unitOfWork.SaveChangesAsync(context.CancellationToken);
        }

        logger.LogInformation(
            "Campaign status update job finished. Started {StartedCount} campaigns, Completed {CompletedCount} campaigns",
            campaignsToStart.Count,
            campaignsToComplete.Count);
    }
}
