using MelTransport.Domain.UserManage.ValueObjects;

namespace MelTransport.Application.UserManage
{
    public interface IPasswordService
    {
        PasswordHash Create(string password);
    }
}
