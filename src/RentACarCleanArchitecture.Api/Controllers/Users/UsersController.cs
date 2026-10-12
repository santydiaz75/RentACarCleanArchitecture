using System.Net;
using Asp.Versioning;
using RentACarCleanArchitecture.Api.Utils;
using RentACarCleanArchitecture.Application.Users.GetUsersDapperPagination;
using RentACarCleanArchitecture.Application.Users.GetUsersPagination;
using RentACarCleanArchitecture.Application.Users.LoginUser;
using RentACarCleanArchitecture.Application.Users.RegisterUser;
using RentACarCleanArchitecture.Domain.Abstractions;
using RentACarCleanArchitecture.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentACarCleanArchitecture.Api.Controllers.Users;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/users")]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [MapToApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> LoginV1(
        [FromBody] LoginUserRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new LoginCommand(request.Email, request.Password);
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return Unauthorized(result.Error);
        }

        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new RegisterUserCommand(
            request.Email,
            request.Nombre,
            request.Apellidos,
            request.Password
        );

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return Unauthorized(result.Error);
        }

        return Ok(result.Value);

    }

    [AllowAnonymous]
    [HttpGet("getPagination", Name ="PaginationUsers")]
    [ProducesResponseType(typeof(PagedResults<User, UserId>),(int)HttpStatusCode.OK
    )]
    public async Task<ActionResult<PagedResults<User, UserId>>> GetPagination(
        [FromQuery] GetUsersPaginationQuery paginationQuery
    )
    {

        var resultados = await _sender.Send(paginationQuery);
        return Ok(resultados);

    }

    [AllowAnonymous]    
    [HttpGet("getPaginationDapper", Name ="GetPaginationDapper")]
    [ProducesResponseType(typeof(PagedDapperResults<UserPaginationData>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PagedDapperResults<UserPaginationData>>> GetPaginationDapper
    (
        [FromQuery] GetUsersDapperPaginationQuery paginationQuery
    ){
        
        var resultados = await _sender.Send(paginationQuery);
        return Ok(resultados);
    }


   

}