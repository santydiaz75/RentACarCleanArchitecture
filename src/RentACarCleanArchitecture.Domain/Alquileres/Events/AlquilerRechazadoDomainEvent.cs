using RentACarCleanArchitecture.Domain.Abstractions;

namespace RentACarCleanArchitecture.Domain.Alquileres.Events;

public sealed record AlquilerRechazadoDomainEvent(AlquilerId Id) : IDomainEvent;