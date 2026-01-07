using CustomerServiceCampaign.Common.Presentation.Endpoints;
using CustomerServiceCampaign.Modules.Campaigns.Application.Abstractions.Customers;
using CustomerServiceCampaign.Modules.Campaigns.Application.Abstractions.Data;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns.Repositories;
using CustomerServiceCampaign.Modules.Campaigns.Domain.Rewards;
using CustomerServiceCampaign.Modules.Campaigns.Infrastructure.Campaigns;
using CustomerServiceCampaign.Modules.Campaigns.Infrastructure.Configuration;
using CustomerServiceCampaign.Modules.Campaigns.Infrastructure.Customers;
using CustomerServiceCampaign.Modules.Campaigns.Infrastructure.Database;
using CustomerServiceCampaign.Modules.Campaigns.Infrastructure.Rewards;
using CustomerServiceCampaign.Modules.Campaigns.Infrastructure.Storage;
using CustomerServiceCampaign.Modules.Campaigns.Application.Abstractions.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;

namespace CustomerServiceCampaign.Modules.Campaigns.Infrastructure;

public static class CampaignsModule
{
    public static IServiceCollection AddCampaignsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CampaignsOptions>(configuration.GetSection("Campaigns"));

        services.AddDbContext<CampaignsDbContext>((sp, options) =>
            options
                .UseSqlServer(
                    configuration.GetConnectionString("customerservicecampaign-db"),
                    sqlServerOptions => sqlServerOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Campaigns)));

        services.AddScoped<ICampaignRepository, CampaignRepository>();
        services.AddScoped<IRewardRepository, RewardRepository>();

        services.AddScoped<IBlobService, BlobService>();

        services
            .AddHttpClient<ICustomerService, CustomerService>((serviceProvider, httpClient) =>
            {
                CampaignsOptions campaignsOptions = serviceProvider
                    .GetRequiredService<IOptions<CampaignsOptions>>().Value;

                httpClient.BaseAddress = new Uri(campaignsOptions.CustomerServiceUrl);
            });

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<CampaignsDbContext>());

        services.ConfigureOptions<ConfigureCampaignsBackgroundJobs>();
    }
}

internal sealed class ConfigureCampaignsBackgroundJobs : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        var campaignStatusJobKey = new JobKey(nameof(CampaignStatusUpdateJob));

        options
            .AddJob<CampaignStatusUpdateJob>(jobBuilder => jobBuilder.WithIdentity(campaignStatusJobKey))
            .AddTrigger(trigger =>
                trigger
                    .ForJob(campaignStatusJobKey)
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInMinutes(1).RepeatForever()));

        var processImportJobKey = new JobKey(nameof(ProcessPurchaseImportJob));

        options
            .AddJob<ProcessPurchaseImportJob>(jobBuilder => jobBuilder.WithIdentity(processImportJobKey))
            .AddTrigger(trigger =>
                trigger
                    .ForJob(processImportJobKey)
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInSeconds(30).RepeatForever()));
    }
}
