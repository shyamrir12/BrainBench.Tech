using Blazor.SubtleCrypto;
using System.Text;

namespace AdminPanelApp.Data.EncryptDecryptServices
{
    public  class EncryptDecrypt: IEncryptDecrypt
    {
        private  readonly ICryptoService _crypto;
        public EncryptDecrypt(ICryptoService crypto)
        {
            _crypto= crypto;
        }
         public async Task< string> Encrypt(string param)
        {
            CryptoResult result = await _crypto.EncryptAsync(param);
            return result.Value;
        }
        public async Task<string> Decrypt(string param)
        {
          return await _crypto.DecryptAsync(replaceSpecialChars(param));
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
