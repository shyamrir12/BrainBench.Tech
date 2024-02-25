namespace AdminPanelApp.Data.EncryptDecryptServices
{
    public interface IEncryptDecrypt
    {
        public  Task<string> Encrypt(string param);
        public Task<string> Decrypt(string param);
    }
}
