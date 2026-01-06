using CustomerServiceCampaign.Common.Domain;
using MediatR;

namespace CustomerServiceCampaign.Common.Application.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
