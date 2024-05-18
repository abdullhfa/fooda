using System.Security.Cryptography;
using System.Text;

namespace CodeFood.Service.Data
{
    public static class Cryptography
    {
        public static string HashPassword(string? password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException(password, "Password is null");
            }

            MD5 md5 = MD5.Create();

            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] byteHash = md5.ComputeHash(bytes);

            string hash = "";

            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }
    }
}
