using MelTransport.Domain.Entities;
using MelTransport.Domain.Ports;
using MelTransport.Infrastructure.Data;

namespace MelTransport.Infrastructure.Repositories
{
    public class PackageRepository : Repository<Package>, IPackageRepository
    {
        public PackageRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}
