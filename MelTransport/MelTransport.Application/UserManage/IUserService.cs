using MelTransport.Application.UserManage.Dto;
using MelTransport.Domain.UserManage;

namespace MelTransport.Application.UserManage
{
    public interface IUserService
    {
        Task<User> Create(UserDto dto);
        Task<User> GetByIdentification(int id);
        Task<IEnumerable<User>> GetUsers();
        Task<User> Update(Guid userID, UserUpdateDto dto);
        Task<bool> Delete (Guid userId);
    }
}
