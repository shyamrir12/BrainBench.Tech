extern alias globalem;
using globalem::Org.BouncyCastle.Crypto.Engines;
using globalem::Org.BouncyCastle.Crypto.Modes;
using globalem::Org.BouncyCastle.Crypto.Parameters;
using System.Security.Cryptography;

namespace IntigrationWeb.Models.sbiIncriptDecript
{
    public class sbiIncriptDecript : IsbiIncriptDecript
    {
        private readonly IConfiguration configuration;

        public sbiIncriptDecript(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetSHA256(string name)
        {

            SHA256 SHA256 = new SHA256CryptoServiceProvider();
            byte[] ba = SHA256.ComputeHash(System.Text.Encoding.Default.GetBytes(name));
            System.Text.StringBuilder hex = new System.Text.StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
        /*Encryption:*/

        public string EncryptWithKey(String messageToEncrypt, byte[] nonSecretPayload = null)
        {
            string SBIENCKeyPath = Convert.ToString(configuration.GetSection("Path").GetSection("SBIENCKey").Value);
            byte[] key = File.ReadAllBytes(SBIENCKeyPath);//File.ReadAllBytes(@"D:/MINERAL_DEPT.key");
            if (string.IsNullOrEmpty(messageToEncrypt))
            {
                throw new ArgumentException("Secret Message Required!", "messageToEncrypt");
            }
            byte[] msgToEncryptByte = System.Text.Encoding.UTF8.GetBytes(messageToEncrypt);

            //Non-secret Payload Optional
            nonSecretPayload = nonSecretPayload ?? new byte[] { };

            //Using random nonce large enough not to repeat
            byte[] cipherText = null;
            byte[] nonce = null;
            var cipher = new GcmBlockCipher(new AesEngine());
            try
            {
                Random rnd = new Random();
                nonce = new byte[16];
                rnd.NextBytes(nonce);
                var parameters = new AeadParameters(new KeyParameter(key), 128, nonce, nonSecretPayload);
                cipher.Init(true, parameters);
                cipherText = new byte[cipher.GetOutputSize(msgToEncryptByte.Length)];
                var len = cipher.ProcessBytes(msgToEncryptByte, 0, msgToEncryptByte.Length, cipherText, 0);
                cipher.DoFinal(cipherText, len);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error {0}", ex.Message);
                Console.ReadKey();
            }

            //Assemble Message
            using (var combinedStream = new MemoryStream())
            {
                using (var binaryWriter = new BinaryWriter(combinedStream))
                {
                    //Prepend Nonce
                    binaryWriter.Write(nonce);
                    //Write Cipher Text
                    binaryWriter.Write(cipherText);
                }
                return Convert.ToBase64String(combinedStream.ToArray());
            }
        }

        public string DecryptWithKey(string encryptedMessage, int nonSecretPayloadLength = 0)
        {
            string SBIENCKeyPath = Convert.ToString(configuration.GetSection("Path").GetSection("SBIENCKey").Value);
            byte[] key = File.ReadAllBytes(SBIENCKeyPath);
            if (encryptedMessage == null || encryptedMessage.Length == 0)
            {
                throw new ArgumentException("Encrypted Message Required!", "encryptedMessage");
            }
            byte[] msgToEncryptByte = Convert.FromBase64String(encryptedMessage);

            using (var cipher_stream = new MemoryStream(msgToEncryptByte))
            using (var cipher_reader = new BinaryReader(cipher_stream))
            {
                var payload = cipher_reader.ReadBytes(nonSecretPayloadLength);
                var nonce = cipher_reader.ReadBytes(16);
                var aesgcm_engine = new GcmBlockCipher(new AesFastEngine());
                var parameters = new AeadParameters(
                    new KeyParameter(key), 128, nonce, payload);
                aesgcm_engine.Init(false, parameters);
                var encrypted_text = cipher_reader.ReadBytes(
                    msgToEncryptByte.Length - (payload.Length + nonce.Length));
                var plaintext_size = aesgcm_engine.GetOutputSize(encrypted_text.Length);
                var plaintext = new byte[plaintext_size];
                var bytes_processed = aesgcm_engine.ProcessBytes(
                    encrypted_text,
                    0,
                    encrypted_text.Length,
                    plaintext,
                    0);
                try
                {
                    aesgcm_engine.DoFinal(plaintext, bytes_processed);
                }

                catch (Exception e)
                {
                    throw e;
                }

                return System.Text.Encoding.UTF8.GetString(plaintext);
            }

        }
    }
}
