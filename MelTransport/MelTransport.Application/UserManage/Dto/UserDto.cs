using MelTransport.Domain.Enums;

namespace MelTransport.Application.UserManage.Dto
{
    public class UserDto
    {
        public TypeId TypeId { get; set; }
        public int IdentificationNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
    }
}
