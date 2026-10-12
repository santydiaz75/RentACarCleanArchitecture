using RentACarCleanArchitecture.Application.Abstractions.Messaging;

namespace RentACarCleanArchitecture.Application.Alquileres.ReservarAlquiler;

public record ReservarAlquilerCommand(
    Guid VehiculoId,
    Guid UserId,
    DateOnly FechaInicio,
    DateOnly FechaFin

) : ICommand<Guid>;