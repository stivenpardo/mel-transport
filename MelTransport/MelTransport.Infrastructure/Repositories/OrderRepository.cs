using MelTransport.Domain.Entities;
using MelTransport.Domain.Ports;
using MelTransport.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MelTransport.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Order> GetByOrderNumberAsync(string orderNumber)
        {
            try
            {
                return await _context.Order
                .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);
            }
            catch (Exception e)
            {

                throw new ArgumentException($"{nameof(GetByOrderNumberAsync)} failed, error: {e.Message}");
            }
        }

        public async Task<int> GetNextOrderNumberSequenceAsync()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                using var command = connection.CreateCommand();
                command.CommandText = "SELECT NEXT VALUE FOR OrderNumberSequence";

                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Failed execution raw SQL to get the next sequence value, the error is: {e.Message}");
            }
        }
    }
}
