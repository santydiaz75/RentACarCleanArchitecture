using RentACarCleanArchitecture.Application.Abstractions.Messaging;
using RentACarCleanArchitecture.Domain.Abstractions;
using RentACarCleanArchitecture.Domain.Shared;
using RentACarCleanArchitecture.Domain.Vehiculos;


namespace RentACarCleanArchitecture.Application.Vehiculos.GetVehiculosKitByPagination;

public record GetVehiculosKitByPaginationQuery : PaginationParams, IQuery<PagedResults<Vehiculo, VehiculoId>>;