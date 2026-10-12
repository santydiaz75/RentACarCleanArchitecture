using RentACarCleanArchitecture.Application.Paginations;
using RentACarCleanArchitecture.Domain.Vehiculos;

namespace RentACarCleanArchitecture.Infrastructure.Repositories;


internal sealed class VehiculoRepository : Repository<Vehiculo, VehiculoId>, IVehiculoRepository, IPaginationVehiculoRepository
{
    public VehiculoRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
