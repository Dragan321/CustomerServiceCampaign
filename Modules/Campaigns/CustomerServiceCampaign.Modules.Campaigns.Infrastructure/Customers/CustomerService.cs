using System.Text;
using System.Xml;
using System.Xml.Serialization;
using CustomerServiceCampaign.Modules.Campaigns.Application.Abstractions.Customers;
using CustomerServiceCampaign.Modules.Campaigns.Infrastructure.Customers.Models;
using Microsoft.Extensions.Logging;

namespace CustomerServiceCampaign.Modules.Campaigns.Infrastructure.Customers;

internal sealed class CustomerService(
    HttpClient httpClient,
    ILogger<CustomerService> logger) : ICustomerService
{
    public async Task<bool> IsValidAsync(int customerId, CancellationToken cancellationToken = default)
    {
        try
        {
            logger.LogInformation("Checking if customer {CustomerId} is valid", customerId);



            var response = await httpClient.GetAsync($"?soap_method=FindPerson&id={customerId}", cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                logger.LogWarning("Customer service returned {StatusCode} for customer {CustomerId}", 
                    response.StatusCode, customerId);
                return false;
            }

            string xmlResponse = await response.Content.ReadAsStringAsync(cancellationToken);
            
            var responseSerializer = new XmlSerializer(typeof(SoapEnvelope<FindPersonResponse>));
            using var sr = new StringReader(xmlResponse);
            var soapResponse = (SoapEnvelope<FindPersonResponse>)responseSerializer.Deserialize(sr)!;

            bool isValid = soapResponse?.Body?.FindPersonResponse?.FindPersonResult?.Name != null;
           
            logger.LogInformation("Customer {CustomerId} is valid:{IsValid}", customerId, isValid);

            return isValid;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while validating customer {CustomerId}", customerId);
            return false;
        }
    }
}
