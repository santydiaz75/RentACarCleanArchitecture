using System.Reflection;
using RentACarCleanArchitecture.Application.Abstractions.Messaging;
using RentACarCleanArchitecture.Domain.Abstractions;
using RentACarCleanArchitecture.Infrastructure;

namespace RentACarCleanArchitecture.ArchitectureTests.Infrastructure;

public class BaseTest
{

    protected static readonly Assembly ApplicationAssembly = typeof(IBaseCommand).Assembly;
    protected static readonly Assembly DomainAssembly = typeof(IEntity).Assembly;

    protected static readonly Assembly InfrastructureAssembly = typeof(ApplicationDbContext).Assembly;

    protected static readonly Assembly PresentationAssembly = typeof(Program).Assembly;

}