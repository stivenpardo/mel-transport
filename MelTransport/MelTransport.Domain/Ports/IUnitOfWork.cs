using MelTransport.Domain.UserManage;

namespace MelTransport.Domain.Ports
{
    public interface IUnitOfWork : IDisposable
    {
        //Repositories
        IBillRepository BillRepository { get; }
        IOrderRepository OrderRepository { get; }
        IPackageRepository PackageRepository { get; }
        ITransporterRepository TransporterRepository { get; }
        IUserRepository UserRepository { get; }
        IVehiculeRepository VehiculeRepository { get; }

        //Persistence

        Task<int> SaveChangesAsync();
        
        //Transactions
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
