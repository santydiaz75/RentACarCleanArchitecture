namespace RentACarCleanArchitecture.Application.Abstractions.Clock;

public interface IDateTimeProvider
{
    DateTime currentTime {get;}
}