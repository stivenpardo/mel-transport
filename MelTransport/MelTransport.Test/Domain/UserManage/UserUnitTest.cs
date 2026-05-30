using MelTransport.Application.UserManage;
using MelTransport.Domain.Enums;
using MelTransport.Domain.UserManage;
using MelTransport.Domain.UserManage.ValueObjects;
using Moq;

namespace MelTransport.Test.Domain.UserManage
{
    public class UserUnitTest
    {
        [Theory]
        [InlineData(TypeId.NIT, 0, "", "", "", "", UserType.Customer)]
        [InlineData(TypeId.NIT, 123, "", "", "", "", UserType.Customer)]
        [InlineData(TypeId.NIT, 123, "carrera 20", "", "", "", UserType.Customer)]
        [InlineData(TypeId.NIT, 123, "carrera 20", "hell", "", "", UserType.Customer)]
        [InlineData(TypeId.NIT, 123, "carrera 20", "hello@hh.com", "", "", UserType.Customer)]
        [InlineData(TypeId.NIT, 123, "carrera 20", "hello@hh.com", "Nnn", "", UserType.Customer)]
        public void Validate_Handle_Errors(
            TypeId typeId,
            int identificationNumber,
            string address,
            string email,
            string firstName,
            string lastName,
            UserType userType)
        {
            //Arrange
            var passwordServiceMock = new Mock<IPasswordService>();

            passwordServiceMock.Setup(p => p.Create(It.IsAny<string>()))
                .Returns(new PasswordHash("testHash", "salthash"));

            //Act 
            var passwordHash = passwordServiceMock.Object.Create("SecurePass123");

            //Assert
            Assert.Throws<ArgumentException>(() =>
            User.Create(
                typeId,
                identificationNumber,
                address,
                email,
                firstName,
                lastName,
                passwordHash,
                userType));
        }


        [Fact]
        public void Create_ValidInput_ReturnsUser()
        {
            //Arrange
            var mockPasswordService = new Mock<IPasswordService>();

            mockPasswordService.Setup(p => p.Create(It.IsAny<string>()))
                .Returns(new PasswordHash("testHash", "salthash"));

            //Act 
            var passwordHash = mockPasswordService.Object.Create("SecurePass123");

            var user = User.Create(
                TypeId.CC,
                123456789,
                "123 Main St",
                "test@example.com",
                "John",
                "Doe",
                passwordHash,
                UserType.Customer
            );

            //assert
            mockPasswordService.Verify(p => p.Create("SecurePass123"), Times.Once);

            Assert.NotNull(user);
            Assert.Equal("John", user.FirstName);
            Assert.Equal("Doe", user.LastName);
        }


        [Fact]
        public void UpdateName_ValidInputs_UpdatesName()
        {
            var user = GetValidUser();

            user.UpdateName("Jane", "Smith");

            Assert.Equal("Jane", user.FirstName);
            Assert.Equal("Smith", user.LastName);
            Assert.NotNull(user.UpdatedAt);
        }

        [Theory]
        [InlineData("", "Doe")]
        [InlineData("John", "")]
        [InlineData("", "")]
        public void UpdateName_InvalidInputs_ThrowsException(string firstName, string lastName)
        {
            var user = GetValidUser();

            Assert.Throws<ArgumentException>(() =>
            {
                user.UpdateName(firstName, lastName);
            });
        }


        [Theory]
        [InlineData("")]
        [InlineData("invalid-email")]
        [InlineData("missingatsymbol.com")]
        public void UpdateEmail_InvalidEmail_ThrowsException(string email)
        {
            var user = GetValidUser();

            Assert.Throws<ArgumentException>(() =>
            {
                user.UpdateEmail(email);
            });
        }

        [Fact]
        public void UpdateEmail_ValidEmail_UpdatesEmail()
        {
            var user = GetValidUser();

            user.UpdateEmail("new@example.com");

            Assert.Equal("new@example.com", user.Email);
            Assert.NotNull(user.UpdatedAt);
        }


        [Fact]
        public void ChangePassword_NullPassword_ThrowsException()
        {
            var user = GetValidUser();

            Assert.Throws<ArgumentNullException>(() =>
            {
                user.ChangePassword(null);
            });
        }

        [Fact]
        public void ChangePassword_ValidPassword_UpdatesPassword()
        {
            //Arrange 
            var passwordServiceMock = new Mock<IPasswordService>();

            passwordServiceMock.Setup(p => p.Create(It.IsAny<string>()))
                .Returns(new PasswordHash("UpdateTetestHash", "Updatesalthash"));

            //Act 
            var newPassword = passwordServiceMock.Object.Create("SecurePass123");

            var user = GetValidUser();

            user.ChangePassword(newPassword);

            Assert.Equal(newPassword.Hash, user.PasswordHash.Hash);
            Assert.Equal(newPassword.Salt, user.PasswordHash.Salt);
            Assert.NotNull(user.UpdatedAt);
        }


        private User GetValidUser()
        {
            var passwordServiceMock = new Mock<IPasswordService>();

            passwordServiceMock.Setup(p => p.Create(It.IsAny<string>()))
                .Returns(new PasswordHash("testHash", "salthash"));

            //Act 
            var passwordHash = passwordServiceMock.Object.Create("SecurePass123");

            return User.Create(
                TypeId.CC,
                123456789,
                "123 Main St",
                "test@example.com",
                "John",
                "Doe",
                passwordHash,
                UserType.Customer
            );
        }

    }
}
