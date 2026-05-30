using MelTransport.Domain.Ports;
using MelTransport.Domain.UserManage;
using Microsoft.EntityFrameworkCore.Storage;

namespace MelTransport.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(
            ApplicationDbContext context,
            IBillRepository billRepository,
            IOrderRepository orderRepository,
            IPackageRepository packageRepository,
            ITransporterRepository transporterRepository,
            IUserRepository userRepository,
            IVehiculeRepository vehiculeRepository)
        {
            _context = context;
            BillRepository = billRepository;
            OrderRepository = orderRepository;
            PackageRepository = packageRepository;
            TransporterRepository = transporterRepository;
            UserRepository = userRepository;
            VehiculeRepository = vehiculeRepository;

        }

        public IBillRepository BillRepository { get; }

        public IOrderRepository OrderRepository { get; }

        public IPackageRepository PackageRepository { get; }

        public ITransporterRepository TransporterRepository { get; }

        public IUserRepository UserRepository { get; }

        public IVehiculeRepository VehiculeRepository { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
