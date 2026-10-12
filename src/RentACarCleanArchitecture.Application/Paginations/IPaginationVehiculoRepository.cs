using System.Linq.Expressions;
using RentACarCleanArchitecture.Domain.Abstractions;
using RentACarCleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore.Query;

namespace RentACarCleanArchitecture.Application.Paginations;

public interface IPaginationVehiculoRepository
{

    Task<PagedResults<Vehiculo, VehiculoId>> GetPaginationAsync(
        Expression<Func<Vehiculo, bool>>? predicate,
        Func<IQueryable<Vehiculo>, IIncludableQueryable<Vehiculo, object>> includes,
        int page, 
        int pageSize,
        string orderBy,
        bool ascending,
        bool disableTracking = true
    );
    
}