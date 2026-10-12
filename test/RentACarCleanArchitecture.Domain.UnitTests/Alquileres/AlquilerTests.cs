using RentACarCleanArchitecture.Domain.Alquileres;
using RentACarCleanArchitecture.Domain.Alquileres.Events;
using RentACarCleanArchitecture.Domain.Shared;
using RentACarCleanArchitecture.Domain.UnitTests.Infrastructure;
using RentACarCleanArchitecture.Domain.UnitTests.Users;
using RentACarCleanArchitecture.Domain.UnitTests.Vehiculos;
using RentACarCleanArchitecture.Domain.Users;
using FluentAssertions;
using Xunit;

namespace RentACarCleanArchitecture.Domain.UnitTests.Alquileres;

public class AlquilerTests : BaseTest
{

    [Fact]
    public void Reserva_Should_RaiseAlquilerReservaDomainEvent()
    {
        //arrange
        var user = User.Create(
            UserMock.Nombre, 
            UserMock.Apellido, 
            UserMock.Email, 
            UserMock.Password
        );

        var precio = new Moneda(10.0m, TipoMoneda.Usd);
        var duracion = DateRange.Create(
            new DateOnly(2024,1,1),
            new DateOnly(2025,1,1)
        );

        var vehiculo = VehiculoMock.Create(precio);
        var precioService = new PrecioService();


        //Act
        var alquiler = Alquiler.Reservar(
            vehiculo,
            user.Id!,
            duracion,
            DateTime.UtcNow,
            precioService
        );


        // Assert

        var alquilerReservaDomainEvent = 
        AssertDomainEventWasPublished<AlquilerReservadoDomainEvent>(alquiler);

        alquilerReservaDomainEvent.AlquilerId.Should().Be(alquiler.Id);
    }
    

}
