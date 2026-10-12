using RentACarCleanArchitecture.Application.Abstractions.Messaging;
using RentACarCleanArchitecture.Application.Paginations;
using RentACarCleanArchitecture.Domain.Abstractions;
using RentACarCleanArchitecture.Domain.Vehiculos;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace RentACarCleanArchitecture.Application.Vehiculos.GetVehiculosKitByPagination;


internal sealed class GetVehiculosKitByPaginationQueryHandler :
IQueryHandler<GetVehiculosKitByPaginationQuery, PagedResults<Vehiculo, VehiculoId>>
{
    private readonly IPaginationVehiculoRepository _paginationVehiculoRepository;

    public GetVehiculosKitByPaginationQueryHandler(IPaginationVehiculoRepository paginationVehiculoRepository)
    {
        _paginationVehiculoRepository = paginationVehiculoRepository;
    }

    public async Task<Result<PagedResults<Vehiculo, VehiculoId>>> Handle(
        GetVehiculosKitByPaginationQuery request, CancellationToken cancellationToken
        )
    {
            var predicateb = PredicateBuilder.New<Vehiculo>(true);

            if(!string.IsNullOrEmpty(request.Search))
            {
                predicateb = predicateb.Or(p => p.Modelo == new Modelo(request.Search));
            }

            return await _paginationVehiculoRepository.GetPaginationAsync(
                predicateb,
                p => p.Include(x => x.Direccion!),
                request.PageNumber,
                request.PageSize,
                request.OrderBy!,
                request.OrderAsc
            );

    }
}