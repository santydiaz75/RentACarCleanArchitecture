using RentACarCleanArchitecture.Domain.Abstractions;

namespace RentACarCleanArchitecture.Domain.Alquileres.Events;

public sealed record AlquilerCanceladoDomainEvent(AlquilerId AlquilerId) : IDomainEvent;