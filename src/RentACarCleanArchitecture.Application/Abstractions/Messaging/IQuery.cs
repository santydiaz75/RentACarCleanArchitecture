using RentACarCleanArchitecture.Domain.Abstractions;
using MediatR;

namespace RentACarCleanArchitecture.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
    
}