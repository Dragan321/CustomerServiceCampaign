using CustomerServiceCampaign.Common.Domain;
using MediatR;

namespace CustomerServiceCampaign.Common.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
