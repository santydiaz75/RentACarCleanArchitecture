using RentACarCleanArchitecture.Domain.Abstractions;

namespace RentACarCleanArchitecture.Domain.Alquileres.Events;


public sealed record AlquilerReservadoDomainEvent(AlquilerId AlquilerId) : IDomainEvent;