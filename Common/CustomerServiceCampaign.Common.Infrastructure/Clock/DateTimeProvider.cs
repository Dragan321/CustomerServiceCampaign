using CustomerServiceCampaign.Common.Application.Clock;

namespace CustomerServiceCampaign.Common.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
