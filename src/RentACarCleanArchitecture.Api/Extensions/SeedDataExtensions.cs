using Bogus;
using RentACarCleanArchitecture.Application.Abstractions.Data;
using RentACarCleanArchitecture.Domain.Permissions;
using RentACarCleanArchitecture.Domain.Roles;
using RentACarCleanArchitecture.Domain.Users;
using RentACarCleanArchitecture.Domain.Vehiculos;
using RentACarCleanArchitecture.Infrastructure;
using Dapper;

namespace RentACarCleanArchitecture.Api.Extensions;

public static class SeedDataExtensions
{
    public static void SeedDataAuthentication(
        this IApplicationBuilder app
    )
    {

        using var scope = app.ApplicationServices.CreateScope();
        var service = scope.ServiceProvider;
        var loggerFactory = service.GetRequiredService<ILoggerFactory>();

        try
        {
            var context = service.GetRequiredService<ApplicationDbContext>();

            if (!context.Set<Role>().Any())
            {
                context.AddRange(Role.GetValues());
            }

            if (!context.Set<Permission>().Any())
            {
                IEnumerable<Permission> permissions = Enum.GetValues<PermissionEnum>()
                .Select(p => new Permission(
                    new PermissionId((int)p),
                    new RentACarCleanArchitecture.Domain.Permissions.Nombre(p.ToString())
                ));

                context.AddRange(permissions);
            }

            if (!context.Set<RolePermission>().Any())
            {
                context.AddRange(
                    new RolePermission { RoleId = Role.Cliente.Id, PermissionId = new PermissionId((int)PermissionEnum.ReadUser) },
                    new RolePermission { RoleId = Role.Admin.Id, PermissionId = new PermissionId((int)PermissionEnum.WriteUser) },
                    new RolePermission { RoleId = Role.Admin.Id, PermissionId = new PermissionId((int)PermissionEnum.UpdateUser) },
                    new RolePermission { RoleId = Role.Admin.Id, PermissionId = new PermissionId((int)PermissionEnum.ReadUser) }
                );
            }

            var adminUser = context.Set<User>().SingleOrDefault(x => x.Email == new Email("santydiaz75@gmail.com"));

            if (adminUser is null)
            {
                var adminPasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123$");

                adminUser = User.Create(
                    new RentACarCleanArchitecture.Domain.Users.Nombre("Admin"),
                    new Apellido("Admin"),
                    new Email("santydiaz75@gmail.com"),
                    new PasswordHash(adminPasswordHash)
                );

                context.Add(adminUser);
            }

            if (!context.Set<UserRole>().Any(x => x.RoleId == Role.Admin.Id && x.UserId == adminUser.Id))
            {
                context.Add(new UserRole
                {
                    RoleId = Role.Admin.Id,
                    UserId = adminUser.Id
                });
            }

            context.SaveChanges();
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<ApplicationDbContext>();
            logger.LogError(ex, "Error al sembrar autenticación");
        }
    }


    public static void SeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
        using var connection = sqlConnectionFactory.CreateConnection();

        if (connection.ExecuteScalar<int>("SELECT COUNT(*) FROM public.vehiculos") > 0)
        {
            return;
        }

        var faker = new Faker();

        List<object> vehiculos = new();

        for(var i = 0; i< 100; i++)
        {
            vehiculos.Add(new 
            {
                Id = Guid.NewGuid(),
                Vin = faker.Vehicle.Vin(),
                Modelo = faker.Vehicle.Model(),
                Pais = faker.Address.Country(),
                Departamento = faker.Address.State(),
                Provincia = faker.Address.County(),
                Ciudad = faker.Address.City(),
                Calle = faker.Address.StreetAddress(),
                PrecioMonto = faker.Random.Decimal(1000, 20000),
                PrecioTipoMoneda =  "USD",
                PrecioMantenimiento = faker.Random.Decimal(100, 200),
                PrecioMantenimientoTipoMoneda = "USD",
                Accesorios = new List<int>{ (int)Accesorio.Wifi, (int)Accesorio.AppleCar},
                FechaUltima = DateTime.MinValue

            });
        }

        const string sql = """
            INSERT INTO public.vehiculos
                (id, vin, modelo, direccion_pais, direccion_departamento, direccion_provincia, direccion_ciudad, direccion_calle, precio_monto, precio_tipo_moneda, mantenimiento_monto, mantenimiento_tipo_moneda, accesorios, fecha_ultima_alquiler)
                values(@id, @Vin,@Modelo,@Pais, @Departamento, @Provincia, @Ciudad, @Calle, @PrecioMonto, @PrecioTipoMoneda, @PrecioMantenimiento, @PrecioMantenimientoTipoMoneda, @Accesorios, @FechaUltima)
        """;

        connection.Execute(sql, vehiculos);
    }


}
