using Read_and_learn.PlatformRelatedServices;
using System.Security.Cryptography;
using System.Text;

namespace Read_and_learn.UWP.PlatformRelatedServices
{
    /// <summary>
    /// Platform-specific implementation of <see cref="ICryptoService"/>.
    /// </summary>
    public class CryptoService : ICryptoService
    {
        public string GetMd5(byte[] bytes)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}
