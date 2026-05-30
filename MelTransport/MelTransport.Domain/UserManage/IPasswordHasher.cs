using MelTransport.Domain.UserManage.ValueObjects;

namespace MelTransport.Domain.UserManage
{
    public interface IPasswordHasher
    {
        PasswordHash Create(string password);
        bool VerifyPassword(string password, PasswordHash storedHash);
    }
}
