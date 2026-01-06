using CustomerServiceCampaign.Common.Domain;
using MediatR;

namespace CustomerServiceCampaign.Common.Application.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

public interface IBaseCommand;
