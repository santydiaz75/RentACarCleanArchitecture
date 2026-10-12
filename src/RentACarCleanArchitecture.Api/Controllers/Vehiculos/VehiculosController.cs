
using System.Net;
using Asp.Versioning;
using RentACarCleanArchitecture.Api.Utils;
using RentACarCleanArchitecture.Application.Vehiculos.GetVehiculosByPagination;
using RentACarCleanArchitecture.Application.Vehiculos.GetVehiculosKitByPagination;
using RentACarCleanArchitecture.Application.Vehiculos.SearchVehiculos;
using RentACarCleanArchitecture.Domain.Abstractions;
using RentACarCleanArchitecture.Domain.Permissions;
using RentACarCleanArchitecture.Domain.Vehiculos;
using RentACarCleanArchitecture.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentACarCleanArchitecture.Api.Controllers.Vehiculos;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/vehiculos")]
public class VehiculosController : ControllerBase
{

    private readonly ISender _sender;

    public VehiculosController(ISender sender)
    {
        _sender = sender;
    }

    [HasPermission(PermissionEnum.ReadUser)]
    [HttpGet("search")]
    public async Task<IActionResult> SearchVehiculos(
        DateOnly startDate,
        DateOnly endDate,
        CancellationToken cancellationToken
    )
    {
        var query = new SearchVehiculosQuery(startDate, endDate);
        var resultados = await _sender.Send(query, cancellationToken);
        return Ok(resultados.Value);
    }

    [AllowAnonymous]
    [HttpGet("getPagination", Name = "PaginationVehiculos")]
    [ProducesResponseType(typeof(PaginationResult<Vehiculo, VehiculoId>), 
        (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginationResult<Vehiculo, VehiculoId>>> GetPaginationVehiculo(
        [FromQuery] GetVehiculosByPaginationQuery request
    )
    {
        var resultados = await _sender.Send(request);
        return Ok(resultados);
    }
    
    [AllowAnonymous]
    [HttpGet("getPaginationKit", Name ="PaginationVehiculoKit")]
    [ProducesResponseType(typeof(PaginationResult<Vehiculo, VehiculoId>),(int)HttpStatusCode.OK)]
    public async Task<ActionResult<PagedResults<Vehiculo, VehiculoId>>> GetPaginationVehiculoKit(
        [FromQuery] GetVehiculosKitByPaginationQuery paginationQuery
    )
    {
        var resultados = await _sender.Send(paginationQuery);
        return Ok(resultados);

    }

}