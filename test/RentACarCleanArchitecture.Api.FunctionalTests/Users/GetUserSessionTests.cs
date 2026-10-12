using System.Net;
using System.Net.Http.Json;
using RentACarCleanArchitecture.Api.FunctionalTests.Infrastructure;
using RentACarCleanArchitecture.Application.Users.LoginUser;
using RentACarCleanArchitecture.Application.Users.RegisterUser;
using FluentAssertions;
using Xunit;

namespace RentACarCleanArchitecture.Api.FunctionalTests.Users;

public class GetUserSessionTests : BaseFunctionalTest
{
    public GetUserSessionTests(FunctionalTestsWebAppFactory factory) : base(factory)
    {
    }

     [Fact]
     public async Task Login_ShouldReturnOk_WhenUserExists()
     {
        //arrange
        var request = new LoginUserRequest(
            UserData.RegisterUserRequestTest.Email,
            UserData.RegisterUserRequestTest.Password
            );


        //act
        var response =  await HttpClient.PostAsJsonAsync("api/v1/users/login", request);


        //assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

     }


     [Fact]
     public async Task Register_ShouldReturnOk_WhenRequestsIsValid()
     {
        //arrange
        var request = new RegisterUserRequest(
                "testx@test.com",
                "testx",
                "testx",
                "Test11233##"
            );


        //act
        var response =  await HttpClient.PostAsJsonAsync("api/v1/users/register", request);


        //assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

     }


}
