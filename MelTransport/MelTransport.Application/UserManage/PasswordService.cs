using MelTransport.Domain.UserManage;
using MelTransport.Domain.UserManage.ValueObjects;

namespace MelTransport.Application.UserManage
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordHasher _passwordHasher;

        public PasswordService(IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public PasswordHash Create(string password)
        {
            return _passwordHasher.Create(password);
        }
    }
}
