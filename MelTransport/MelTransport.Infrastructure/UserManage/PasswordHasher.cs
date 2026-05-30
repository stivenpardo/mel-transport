using MelTransport.Domain.UserManage;
using MelTransport.Domain.UserManage.ValueObjects;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace MelTransport.Infrastructure.UserManage
{
    public class PasswordHasher : IPasswordHasher
    {
        public PasswordHash Create(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                throw new ArgumentException("Password must be at least 8 characters", nameof(password));

            // Generate a salt
            byte[] saltBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            string salt = Convert.ToBase64String(saltBytes);

            // Hash the password
            string hashed = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: saltBytes,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

            return new PasswordHash(hashed, salt);
        }

        public bool VerifyPassword(string password, PasswordHash storedHash)
        {
            byte[] saltBytes = Convert.FromBase64String(storedHash.Salt);

            string hashed = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: saltBytes,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

            return storedHash.Hash == hashed;
        }
    }
}
