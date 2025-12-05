using System.Buffers.Text;
using System.Security.Cryptography;
using System.Text;
namespace Ninwa_Employee.Data
{
    public static class EncryptionHelper
    {
        private static readonly (byte[] Key, byte[] IV) KeyAndIV = KeyGenerator.GenerateKeyAndIV();

        public static string EncryptString(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = KeyAndIV.Key;
            aes.IV = KeyAndIV.IV;

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }
            return Convert.ToBase64String(ms.ToArray());
        }

        public static string DecryptString(string cipherText)
        {
            cipherText = SanitizeBase64String(cipherText);
            //cipherText = AddPaddingToBase64(cipherText);

            if (!IsBase64String(cipherText))
            {
                throw new ArgumentException("The input is not a valid Base-64 string.");
            }

            using var aes = Aes.Create();
            aes.Key = KeyAndIV.Key;
            aes.IV = KeyAndIV.IV;

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using var ms = new MemoryStream(Convert.FromBase64String(cipherText));
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }

        //private static string AddPaddingToBase64(string base64)
        //{
        //    int padding = 4 - (base64.Length % 4);
        //    if (padding < 4)
        //    {
        //        base64 = base64.PadRight(base64.Length + padding, '=');
        //    }
        //    return base64;
        //}

        private static string SanitizeBase64String(string base64)
        {
            base64 = base64.Replace("&#x2B;", "+") // HTML entity for '+'
                           .Replace("&#x2F;", "/") // HTML entity for '/'
                           .Replace("&#x3D;", "="); // HTML entity for '='
            return base64;
        }

        public static bool IsBase64String(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            // Replace HTML entities
            value = SanitizeBase64String(value);

            // Ensure length is a multiple of 4
            if (value.Length % 4 != 0)
                return false;

            // Check if it can be decoded
            try
            {
                Convert.FromBase64String(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public static class KeyGenerator
    {
        public static (byte[] Key, byte[] IV) GenerateKeyAndIV()
        {
            using var aes = Aes.Create();
            aes.GenerateKey();
            aes.GenerateIV();
            return (aes.Key, aes.IV);
        }
    }

}
