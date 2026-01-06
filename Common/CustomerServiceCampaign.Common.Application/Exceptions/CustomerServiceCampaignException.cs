using CustomerServiceCampaign.Common.Domain;

namespace CustomerServiceCampaign.Common.Application.Exceptions;

public sealed class CustomerServiceCampaignException : Exception
{
    public CustomerServiceCampaignException(string requestName, Error? error = default, Exception? innerException = default)
        : base("Application exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }

    public string RequestName { get; }

    public Error? Error { get; }
}
