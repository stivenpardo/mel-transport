using MelTransport.Domain.Entities;

namespace MelTransport.Domain.Ports
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetByOrderNumberAsync(string orderNumber);
        Task<int> GetNextOrderNumberSequenceAsync();
    }
}
