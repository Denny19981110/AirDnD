using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Airdnd.Core.Helper
{
    public static class Encryption
    {
        public static string SHA256Encrypt(this string strSource)
        {
            byte[] source = Encoding.Default.GetBytes(strSource);
            byte[] crypto = new SHA256CryptoServiceProvider().ComputeHash(source);
            string result = crypto.Aggregate(string.Empty, (current, t) => current + t.ToString("X2"));

            return result.ToUpper();
        }
    }
}
