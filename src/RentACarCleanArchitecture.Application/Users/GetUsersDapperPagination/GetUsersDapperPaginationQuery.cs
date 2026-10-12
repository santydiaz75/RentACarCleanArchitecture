using RentACarCleanArchitecture.Application.Abstractions.Messaging;
using RentACarCleanArchitecture.Domain.Abstractions;
using RentACarCleanArchitecture.Domain.Shared;

namespace RentACarCleanArchitecture.Application.Users.GetUsersDapperPagination;

public sealed record GetUsersDapperPaginationQuery 
: PaginationParams, IQuery<PagedDapperResults<UserPaginationData>>;
