using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace AspisNet.Security {
	public class PasswordHasher {

		private const int SALT_BYTE_COUNT = 128 / 8;
		private const int HASH_BYTE_COUNT = 256 / 8;
		private const int HASH_ITERATIONS = 1;


        public static string HashPassword(string password, out byte[] salt)
		{
			//Generate our salt for the hash
            salt = RandomNumberGenerator.GetBytes(SALT_BYTE_COUNT);
			byte[] key = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, HASH_ITERATIONS, HASH_BYTE_COUNT);

			return Convert.ToBase64String(key);
		}

        public static string HashPasswordWithKnownSalt(string password, byte[] salt)
        {
            byte[] key = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, HASH_ITERATIONS, HASH_BYTE_COUNT);
            return Convert.ToBase64String(key);
        }

    }
}
