using MelTransport.Domain.Entities;

namespace MelTransport.Domain.Ports
{
    public interface IBillRepository : IRepository<Bill>
    {
        Task<Bill> GetByBillNumberAsync(string billNumber);
        Task<int> GetNextBillNumberSequenceAsync();
    }
}
