using System.Linq.Expressions;
using RentACarCleanArchitecture.Domain.Abstractions;
using RentACarCleanArchitecture.Domain.Users;
using Microsoft.EntityFrameworkCore.Query;

namespace RentACarCleanArchitecture.Application.Paginations;

public interface IPaginationRepository
{

    Task<PagedResults<User, UserId>> GetPaginationAsync(
        Expression<Func<User, bool>>? predicate,
        Func<IQueryable<User>, IIncludableQueryable<User, object>> includes,
        int page, 
        int pageSize,
        string orderBy,
        bool ascending,
        bool disableTracking = true
    );
    
}