using RentACarCleanArchitecture.Domain.Abstractions;

namespace RentACarCleanArchitecture.Domain.Alquileres.Events;

public sealed record AlquilerConfirmadoDomainEvent(AlquilerId AlquilerId) : IDomainEvent;