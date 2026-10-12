using RentACarCleanArchitecture.Application.Abstractions.Clock;

namespace RentACarCleanArchitecture.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime currentTime => DateTime.UtcNow;
}