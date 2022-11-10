using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal class PasswordControl
    {
        public static string ComputeHash(byte[] bytesToHash, byte[] salt)
        {
            var byteResult = new Rfc2898DeriveBytes(bytesToHash, salt, 3000);
            return Convert.ToBase64String(byteResult.GetBytes(24));
        }

        public static string[] GenerateHashedPassword(string pwd)
        {
            string[] result = new string[2];
            result[0] = PasswordControl.GenerateSalt();
            result[1] = PasswordControl.ComputeHash(Encoding.UTF8.GetBytes(pwd), Encoding.UTF8.GetBytes(result[0]));
            return result;
        }

        public static string GenerateSalt()
        {
            var bytes = new byte[128 / 8];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        public static bool ValidatePassword(string inp_pw, string salt, string true_pw)
        {

            var hash = ComputeHash(Encoding.UTF8.GetBytes(inp_pw), Encoding.UTF8.GetBytes(salt));
            var true_hash = true_pw;

            if (hash == true_hash)
                return true;
            return false;
        }
    }

}
