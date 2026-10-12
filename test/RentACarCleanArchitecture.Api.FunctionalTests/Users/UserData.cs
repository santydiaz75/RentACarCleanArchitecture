using RentACarCleanArchitecture.Application.Users.RegisterUser;

namespace RentACarCleanArchitecture.Api.FunctionalTests.Users;

internal static class UserData
{

    public static RegisterUserRequest RegisterUserRequestTest 
    = new("felipe.rosas@test.com", "Felipe","Rosas", "Test123$");

}