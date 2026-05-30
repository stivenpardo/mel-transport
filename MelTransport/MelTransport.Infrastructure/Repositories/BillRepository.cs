using MelTransport.Domain.Entities;
using MelTransport.Domain.Ports;
using MelTransport.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MelTransport.Infrastructure.Repositories
{
    public class BillRepository : Repository<Bill>, IBillRepository
    {
        public BillRepository(ApplicationDbContext contex) : base(contex)
        {
            
        }

        public async Task<Bill> GetByBillNumberAsync(string billNumber)
        {
            try
            {
                return await _context.Bill
                .FirstOrDefaultAsync(b => b.BillNumber == billNumber);
            }
            catch (Exception e)
            {

                throw new ArgumentException($"{nameof(GetByBillNumberAsync)} failed, error: {e.Message}");
            }
        }

        public async Task<int> GetNextBillNumberSequenceAsync()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                using var command = connection.CreateCommand();
                command.CommandText = "SELECT NEXT VALUE FOR BillNumberSequence";

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
