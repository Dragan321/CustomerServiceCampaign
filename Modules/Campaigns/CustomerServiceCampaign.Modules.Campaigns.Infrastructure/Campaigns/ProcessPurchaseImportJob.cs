using System.Globalization;
using CustomerServiceCampaign.Modules.Campaigns.Application.Abstractions.Data;
using CustomerServiceCampaign.Modules.Campaigns.Application.Abstractions.Storage;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns.Repositories;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Rewards;
using Microsoft.Extensions.Logging;
using Quartz;

namespace CustomerServiceCampaign.Modules.Campaigns.Infrastructure.Campaigns;

[DisallowConcurrentExecution]
internal sealed class ProcessPurchaseImportJob(
    ICampaignRepository campaignRepository,
    IRewardRepository rewardRepository,
    IBlobService blobService,
    IUnitOfWork unitOfWork,
    ILogger<ProcessPurchaseImportJob> logger) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Starting purchase import processing job");

        PurchaseImport? purchaseImport = await campaignRepository.GetNextPendingImportAsync(context.CancellationToken);

        if (purchaseImport is null)
        {
            logger.LogInformation("No pending imports found");
            return;
        }

        try
        {
            purchaseImport.MarkAsProcessing();
            await unitOfWork.SaveChangesAsync(context.CancellationToken);

            using Stream stream = await blobService.DownloadAsync(purchaseImport.BlobName!, context.CancellationToken);
            using var reader = new StreamReader(stream);
            
            // Very simple CSV parsing for this demo/scenario
            // Expected format: CustomerId,PurchaseDate
            string? header = await reader.ReadLineAsync(context.CancellationToken);
            
            List<Reward> rewards = await rewardRepository.GetByCampaignIdAsync(purchaseImport.CampaignId, context.CancellationToken);
            var rewardsByCustomerId = rewards.ToDictionary(r => r.CustomerId);

            int processedCount = 0;
            int matchedCount = 0;

            while (await reader.ReadLineAsync(context.CancellationToken) is { } line)
            {
                processedCount++;
                string[] parts = line.Split(',');
                if (parts.Length < 2) continue;

                if (int.TryParse(parts[0], out int customerId) && 
                    DateTime.TryParse(parts[1], CultureInfo.InvariantCulture, out DateTime purchaseDate))
                {
                    if (rewardsByCustomerId.TryGetValue(customerId, out Reward? reward))
                    {
                        reward.MarkAsConverted(purchaseDate);
                        matchedCount++;
                    }
                }
            }

            purchaseImport.MarkAsCompleted();
            await unitOfWork.SaveChangesAsync(context.CancellationToken);

            logger.LogInformation(
                "Import {ImportId} completed. Processed {ProcessedCount} rows, Matched {MatchedCount} rewards",
                purchaseImport.Id,
                processedCount,
                matchedCount);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to process import {ImportId}", purchaseImport.Id);
            purchaseImport.MarkAsFailed();
            await unitOfWork.SaveChangesAsync(context.CancellationToken);
        }
    }
}
