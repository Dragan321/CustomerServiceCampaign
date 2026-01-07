namespace CustomerServiceCampaign.Modules.Campaigns.Application.Abstractions.Customers;

public interface ICustomerService
{
    Task<bool> IsValidAsync(int customerId, CancellationToken cancellationToken = default);
}
