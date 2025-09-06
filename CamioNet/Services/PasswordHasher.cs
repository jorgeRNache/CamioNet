using System.Security.Cryptography;
using System.Text;

namespace camionet.Services
{
    public class PasswordHasher 
    {
        // Método para encriptar la contraseña con MD5
        public static string HashPassword(string password)
        {
            using (var md5 = MD5.Create())
            {
                // Convertimos la contraseña en bytes
                var inputBytes = Encoding.ASCII.GetBytes(password);
                var hashBytes = md5.ComputeHash(inputBytes);

                // Convertimos los bytes hash a una cadena hexadecimal
                var sb = new StringBuilder();
                foreach (var byteValue in hashBytes)
                {
                    sb.Append(byteValue.ToString("X2"));
                }

                return sb.ToString(); // Contraseña encriptada en formato MD5
            }
        }
    }
}
