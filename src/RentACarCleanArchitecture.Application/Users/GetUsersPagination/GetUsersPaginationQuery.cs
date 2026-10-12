using RentACarCleanArchitecture.Application.Abstractions.Messaging;
using RentACarCleanArchitecture.Domain.Abstractions;
using RentACarCleanArchitecture.Domain.Shared;
using RentACarCleanArchitecture.Domain.Users;

namespace RentACarCleanArchitecture.Application.Users.GetUsersPagination;

public record GetUsersPaginationQuery 
: PaginationParams, IQuery<PagedResults<User, UserId>>
{
    
}