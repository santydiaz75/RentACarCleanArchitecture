using RentACarCleanArchitecture.Application.Abstractions.Messaging;
using RentACarCleanArchitecture.Domain.Abstractions;
using RentACarCleanArchitecture.Domain.Shared;
using RentACarCleanArchitecture.Domain.Vehiculos;

namespace RentACarCleanArchitecture.Application.Vehiculos.GetVehiculosByPagination;

public sealed record GetVehiculosByPaginationQuery 
: SpecificationEntry, IQuery<PaginationResult<Vehiculo, VehiculoId>>
{
    public string? Modelo {get; init;}
}