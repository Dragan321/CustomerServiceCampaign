using CustomerServiceCampaign.Common.Domain;

namespace CustomerServiceCampaign.Modules.Campaigns.Domain.Campaigns;

public sealed class Campaign : Entity
{
    private Campaign()
    {
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public DateTime StartDate { get; private set; }

    public int LengthInDays { get; private set; }

    public DateTime EndDate { get; private set; }

    public CampaignStatus Status { get; private set; }

    public static Campaign Create(string name, DateTime startDate, int lengthInDays)
    {
        var campaign = new Campaign
        {
            Id = Guid.NewGuid(),
            Name = name,
            StartDate = startDate,
            LengthInDays = lengthInDays,
            EndDate = startDate.AddDays(lengthInDays),
            Status = CampaignStatus.Draft
        };

        return campaign;
    }

    public Result Start()
    {
        if (Status != CampaignStatus.Draft)
        {
            return Result.Failure(CampaignErrors.InvalidStatusTransition);
        }

        Status = CampaignStatus.Active;

        return Result.Success();
    }

    public Result Cancel()
    {
        if (Status == CampaignStatus.Completed || Status == CampaignStatus.Cancelled)
        {
            return Result.Failure(CampaignErrors.InvalidStatusTransition);
        }

        Status = CampaignStatus.Cancelled;

        return Result.Success();
    }

    public Result Complete()
    {
        if (Status != CampaignStatus.Active)
        {
            return Result.Failure(CampaignErrors.InvalidStatusTransition);
        }

        Status = CampaignStatus.Completed;

        return Result.Success();
    }
}
