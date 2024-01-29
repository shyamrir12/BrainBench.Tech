using System.Security.Cryptography;
using System.Text;

namespace IntigrationWeb.Models.EncryptDecrypt
{
    public class EncryptDecrypts
    {
        public string EncryptAES(string cipherString, string PrivateKey = "i&4d$v3@ibank15s")
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(cipherString);
            string key = PrivateKey;
            keyArray = UTF8Encoding.UTF8.GetBytes(key);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.KeySize = 128;
            aes.Key = keyArray;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.ECB;
            ICryptoTransform cTransform = aes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            aes.Clear();
            return Convert.ToBase64String(resultArray);
        }


        public string DecryptAES(string cipherString, string PrivateKey = "i&4d$v3@ibank15s")
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(replaceSpecialChars(cipherString));
            string key = PrivateKey;
            keyArray = UTF8Encoding.UTF8.GetBytes(key);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.KeySize = 128;
            aes.Key = keyArray;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.ECB;
            ICryptoTransform cTransform = aes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            aes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }


        private static String replaceSpecialChars(string str)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in str)
            {
                if (c == ' ')
                    sb.Append('+');
                else
                    sb.Append(c);

            }

            return sb.ToString();
        }
    }
}
