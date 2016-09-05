using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    using System;
    using System.Text;
    using System.Security.Cryptography;

    namespace PasswordHash
    {
        /// <summary>
        /// Salted password hashing with PBKDF2-SHA1.
        /// Author: havoc AT defuse.ca
        /// www: http://crackstation.net/hashing-security.htm
        /// Compatibility: .NET 3.0 and later.
        /// </summary>
        public class PasswordHash
        {
            // The following constants may be changed without breaking existing hashes.
            /// <summary>
            /// Creates a salted PBKDF2 hash of the password.
            /// </summary>
            /// <param name="password">The password to hash.</param>
            /// <returns>The hash of the password.</returns>
            public static string EncryptPassword(string pass, int passwordFormat, HashAlgorithm algorithm)
            {
                string salt = "Zan8iwiiQQbWDRId/KpX4g==";
                if (passwordFormat == 0) // MembershipPasswordFormat.Clear
                    return pass;

                byte[] bIn = Encoding.Unicode.GetBytes(pass);
                byte[] bSalt = Convert.FromBase64String(salt);
                byte[] bRet = null;

                if (passwordFormat == 1)
                { // MembershipPasswordFormat.Hashed 
                    HashAlgorithm hm = algorithm;
                    if (hm is KeyedHashAlgorithm)
                    {
                        KeyedHashAlgorithm kha = (KeyedHashAlgorithm)hm;
                        if (kha.Key.Length == bSalt.Length)
                        {
                            kha.Key = bSalt;
                        }
                        else if (kha.Key.Length < bSalt.Length)
                        {
                            byte[] bKey = new byte[kha.Key.Length];
                            Buffer.BlockCopy(bSalt, 0, bKey, 0, bKey.Length);
                            kha.Key = bKey;
                        }
                        else
                        {
                            byte[] bKey = new byte[kha.Key.Length];
                            for (int iter = 0; iter < bKey.Length; )
                            {
                                int len = Math.Min(bSalt.Length, bKey.Length - iter);
                                Buffer.BlockCopy(bSalt, 0, bKey, iter, len);
                                iter += len;
                            }
                            kha.Key = bKey;
                        }
                        bRet = kha.ComputeHash(bIn);
                    }
                    else
                    {
                        byte[] bAll = new byte[bSalt.Length + bIn.Length];
                        Buffer.BlockCopy(bSalt, 0, bAll, 0, bSalt.Length);
                        Buffer.BlockCopy(bIn, 0, bAll, bSalt.Length, bIn.Length);
                        bRet = hm.ComputeHash(bAll);
                    }
                }

                return Convert.ToBase64String(bRet);
            }
        }
    }
}