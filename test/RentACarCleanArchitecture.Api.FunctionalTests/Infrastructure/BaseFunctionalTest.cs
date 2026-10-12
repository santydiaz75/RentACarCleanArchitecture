using System.Net.Http.Json;
using RentACarCleanArchitecture.Api.FunctionalTests.Users;
using RentACarCleanArchitecture.Application.Users.LoginUser;
using Xunit;

namespace RentACarCleanArchitecture.Api.FunctionalTests.Infrastructure;

public abstract class BaseFunctionalTest :
IClassFixture<FunctionalTestsWebAppFactory>
{

    protected readonly HttpClient HttpClient;

    protected BaseFunctionalTest(FunctionalTestsWebAppFactory factory)
    {
        HttpClient = factory.CreateClient();
    }

    protected async Task<string> GetToken()
    {
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(
            "api/v1/users/login",
            new LoginUserRequest(
                UserData.RegisterUserRequestTest.Email,
                UserData.RegisterUserRequestTest.Password
            )
        );

        return await response.Content.ReadAsStringAsync();
    }



}