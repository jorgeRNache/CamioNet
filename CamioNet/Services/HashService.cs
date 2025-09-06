using System.Security.Cryptography;
using System.Text;

namespace camionet.Services
{
    public class HashService
    {
        public string CalcularSHA256(string input)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToHexString(hash); // o BitConverter.ToString(hash).Replace("-", "")
        }
    }

}
