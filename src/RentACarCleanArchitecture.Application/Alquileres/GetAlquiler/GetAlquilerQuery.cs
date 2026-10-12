using RentACarCleanArchitecture.Application.Abstractions.Messaging;

namespace RentACarCleanArchitecture.Application.Alquileres.GetAlquiler;


public sealed record GetAlquilerQuery(Guid AlquilerId) : IQuery<AlquilerResponse>;
