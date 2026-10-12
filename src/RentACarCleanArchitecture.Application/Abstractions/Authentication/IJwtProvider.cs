using RentACarCleanArchitecture.Domain.Users;

namespace RentACarCleanArchitecture.Application.Abstractions.Authentication;

public interface IJwtProvider
{

    Task<string> Generate(User user);

}