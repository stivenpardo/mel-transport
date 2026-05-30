using MelTransport.Domain.Entities;
using MelTransport.Domain.Ports;
using MelTransport.Infrastructure.Data;

namespace MelTransport.Infrastructure.Repositories
{
    public class TransporterRepository : Repository<Transporter>, ITransporterRepository
    {
        public TransporterRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
