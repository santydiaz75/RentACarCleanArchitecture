using RentACarCleanArchitecture.Domain.Abstractions;

namespace RentACarCleanArchitecture.Domain.Reviews.Events;

public sealed record ReviewCreatedDomainEvent(ReviewId ReviewId) : IDomainEvent;