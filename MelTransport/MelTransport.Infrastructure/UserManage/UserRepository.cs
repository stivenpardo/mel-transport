using MelTransport.Domain.UserManage;
using MelTransport.Infrastructure.Data;
using MelTransport.Infrastructure.Repositories;

namespace MelTransport.Infrastructure.UserManage
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext contex) : base(contex)
        {            
        }
    }
}
