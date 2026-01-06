using Microsoft.AspNetCore.Routing;

namespace CustomerServiceCampaign.Common.Presentation.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
