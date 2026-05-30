using MelTransport.Domain.Entities;
using MelTransport.Domain.Ports;
using MelTransport.Infrastructure.Data;

namespace MelTransport.Infrastructure.Repositories
{
    public class VehiculeRepository : Repository<Vehicle>, IVehiculeRepository
    {
        public VehiculeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
