using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace VoorraadBeheer.Authentication.Cryptography
{
    public class CryptographyHelper
    {
        public byte[] CreateSalt()
        {
            var buffer = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);
            return buffer;
        }

        public byte[] HashPassword(string password, byte[] salt)
        {
            byte[] hash;
            using (var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password)))
            {
                argon2.Salt = salt;
                argon2.DegreeOfParallelism = 8; // four cores
                argon2.Iterations = 4;
                argon2.MemorySize = 1024 * 1024; // 1 GB
                hash = argon2.GetBytes(32);
            }
            return hash;
        }

        public bool VerifyHash(string password, byte[] salt, byte[] hash)
        {
            var newHash = HashPassword(password, salt);
            return hash.SequenceEqual(newHash);
        }
    }
}
