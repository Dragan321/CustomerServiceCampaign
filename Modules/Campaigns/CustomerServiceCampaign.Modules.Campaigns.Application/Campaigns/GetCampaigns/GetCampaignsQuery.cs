using CustomerServiceCampaign.Common.Application.Messaging;

namespace CustomerServiceCampaign.Modules.Campaigns.Application.Campaigns.GetCampaigns;

public sealed record GetCampaignsQuery(int Page, int PageSize) : IQuery<PagedList<CampaignResponse>>;
