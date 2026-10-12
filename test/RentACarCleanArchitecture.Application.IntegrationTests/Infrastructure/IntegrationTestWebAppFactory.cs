using RentACarCleanArchitecture.Application.Abstractions.Data;
using RentACarCleanArchitecture.Infrastructure;
using RentACarCleanArchitecture.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;
using Xunit;

namespace RentACarCleanArchitecture.Application.IntegrationTests;
public class IntegrationTestWebAppFactory : WebApplicationFactory<global::Program>, IAsyncLifetime
{
    
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
    .WithImage("postgres:16.0")
    .WithDatabase("rentacarcleanarchitecturedb")
    .WithUsername("testuser")
    .WithPassword("testpwd")
    .Build();
    


    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
       await _dbContainer.StopAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services => {

            services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));

            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseNpgsql(_dbContainer.GetConnectionString())
                .UseSnakeCaseNamingConvention()
            );

            services.RemoveAll(typeof(ISqlConnectionFactory));
            services.AddSingleton<ISqlConnectionFactory>(_ => 
                new SqlConnectionFactory(_dbContainer.GetConnectionString())
            );

        });
        
        
        base.ConfigureWebHost(builder);
    }


}
