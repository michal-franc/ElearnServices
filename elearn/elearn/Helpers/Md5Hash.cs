using System;
using System.Security.Cryptography;
using System.Text;

namespace elearn.Helpers
{
    public static class Md5Hash
    {
        public  static string EncodePassword(string originalPassword)
        {

            MD5 md5 = new MD5CryptoServiceProvider();
            var originalBytes = Encoding.Default.GetBytes(originalPassword);
            var encodedBytes = md5.ComputeHash(originalBytes);

            return BitConverter.ToString(encodedBytes);
        }
    }
}
