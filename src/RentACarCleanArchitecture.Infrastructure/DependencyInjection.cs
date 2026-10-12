
using RentACarCleanArchitecture.Application.Abstractions.Clock;
using RentACarCleanArchitecture.Application.Abstractions.Data;
using RentACarCleanArchitecture.Application.Abstractions.Email;
using RentACarCleanArchitecture.Application.Paginations;
using RentACarCleanArchitecture.Domain.Abstractions;
using RentACarCleanArchitecture.Domain.Alquileres;
using RentACarCleanArchitecture.Domain.Users;
using RentACarCleanArchitecture.Domain.Vehiculos;
using RentACarCleanArchitecture.Infrastructure.Clock;
using RentACarCleanArchitecture.Infrastructure.Data;
using RentACarCleanArchitecture.Infrastructure.Outbox;
using RentACarCleanArchitecture.Infrastructure.Repositories;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace RentACarCleanArchitecture.Infrastructure;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
        )
    {

        services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));

        OutboxOptions outboxOptions = configuration.GetSection("Outbox").Get<OutboxOptions>() ?? new OutboxOptions();

        services.AddQuartz(options =>
        {
            const string jobName = nameof(InvokeOutboxMessagesJob);

            options.AddJob<InvokeOutboxMessagesJob>(configure => configure.WithIdentity(jobName));

            options.AddTrigger(configure =>
                configure
                    .ForJob(jobName)
                    .WithIdentity($"{jobName}-trigger")
                    .WithSimpleSchedule(schedule =>
                        schedule.WithInterval(TimeSpan.FromSeconds(outboxOptions.IntervalInSeconds)).RepeatForever()));
        });

        services.AddQuartzHostedService(options =>
            options.WaitForJobsToComplete = true
        );

        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IEmailService, EmailService>();

        var connectionString = configuration.GetConnectionString("Database") 
             ?? throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options => {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPaginationRepository, UserRepository>();
        services.AddScoped<IPaginationVehiculoRepository, VehiculoRepository>();
       

        services.AddScoped<IVehiculoRepository, VehiculoRepository>();
        services.AddScoped<IAlquilerRepository, AlquilerRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

services
.AddSingleton<ISqlConnectionFactory>( _ => new SqlConnectionFactory(connectionString));

        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        return services;
    }

}