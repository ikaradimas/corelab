using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace CoreLab.App {
    public static class PasswordHash {
        private const int SaltByteSize = 24;
        private const int HashByteSize = 24;
        private const int Pbkdf2Iterations = 1000;
        private const int SaltIndex = 0;
        private const int Pbkdf2Index = 1;

        public static string CreateHash (string password) {
            // Generate a random salt
            using (var csprng = new RNGCryptoServiceProvider ()) {
                byte[] salt = new byte[SaltByteSize];
                csprng.GetBytes (salt);

                // Hash the password and encode the parameters
                byte[] hash = Pbkdf2 (password, salt, HashByteSize);
                return Convert.ToBase64String (salt) + ":" +
                    Convert.ToBase64String (hash);
            }
        }

        /// <summary>
        /// Validates a password given a hash of the correct one.
        /// </summary>
        public static bool ValidatePassword (string password, string correctHash) {
            // Extract the parameters from the hash
            char[] delimiter = { ':' };
            string[] split = correctHash.Split (delimiter);
            if (split.Count () < 2) return false;
            byte[] salt = Convert.FromBase64String (split[SaltIndex]);
            byte[] hash = Convert.FromBase64String (split[Pbkdf2Index]);
            byte[] testHash = Pbkdf2 (password, salt, hash.Length);
            return SlowEquals (hash, testHash);
        }

        /// <summary>
        /// Compares two byte arrays in length-constant time. This comparison
        /// method is used so that password hashes cannot be extracted from
        /// on-line systems using a timing attack and then attacked off-line.
        /// </summary>
        private static bool SlowEquals (byte[] a, byte[] b) {
            uint diff = (uint) a.Length ^ (uint) b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint) (a[i] ^ b[i]);
            return diff == 0;
        }

        private static byte[] Pbkdf2 (string password, byte[] salt, int outputBytes) {
            using (var pbkdf2 = new Rfc2898DeriveBytes (password, salt)) {
                pbkdf2.IterationCount = Pbkdf2Iterations;
                return pbkdf2.GetBytes (outputBytes);
            }
        }
    }
}