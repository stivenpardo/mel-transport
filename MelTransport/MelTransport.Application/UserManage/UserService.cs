using MelTransport.Application.UserManage.Dto;
using MelTransport.Domain.Ports;
using MelTransport.Domain.UserManage;

namespace MelTransport.Application.UserManage
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _passwordService;

        public UserService(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
        }

        public async Task<User> Create(UserDto dto)
        {
            var passwordHash = _passwordService.Create(dto.Password);

            var user = User.Create(
                dto.TypeId,
                dto.IdentificationNumber,
                dto.Address,
                dto.Email,
                dto.FirstName,
                dto.LastName,
                passwordHash,
                dto.UserType
            );

            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return user;
        }

        public async Task<User> Update(Guid userId, UserUpdateDto dto)
        {
            var user = await _userRepository.GetByIdAsync(userId) ?? throw new Exception("User not found");

            user.UpdateName(dto.FirstName, dto.LastName);
            user.UpdateEmail(dto.Email);

            if (!string.IsNullOrWhiteSpace(dto.NewPassword))
            {
                var newPasswordHash = _passwordService.Create(dto.NewPassword);
                user.ChangePassword(newPasswordHash);
            }

            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();

            return user;
        }

        public async Task<bool> Delete(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            
            if (user == null)
                return false;

            _userRepository.Remove(user);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<User> GetByIdentification(int id)
        {
            var users = await _userRepository.FindAsync(u => u.IdentificationNumber == id);
            return users.FirstOrDefault() ?? throw new NullReferenceException("The identification number does not exist");
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _userRepository.GetAllAsync();
            return users;
        }

    }
}
