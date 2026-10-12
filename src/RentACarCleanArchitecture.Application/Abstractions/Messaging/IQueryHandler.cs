using RentACarCleanArchitecture.Domain.Abstractions;
using MediatR;

namespace RentACarCleanArchitecture.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> 
: IRequestHandler<TQuery, Result<TResponse>>
where TQuery : IQuery<TResponse>
{

}