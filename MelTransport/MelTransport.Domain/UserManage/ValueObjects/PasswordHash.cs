namespace MelTransport.Domain.UserManage.ValueObjects
{
    public record PasswordHash
    {
        public string Hash { get; }
        public string Salt { get; }

        private PasswordHash() { }

        public PasswordHash(string hash, string salt)
        {
            if (string.IsNullOrWhiteSpace(hash))
                throw new ArgumentException("Hash value cannot be empty", nameof(hash));

            Hash = hash;
            Salt = salt;
        }
    }
}
