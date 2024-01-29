namespace IntigrationWeb.Models.sbiIncriptDecript
{
    public interface IsbiIncriptDecript
    {
        string GetSHA256(string name);
        string EncryptWithKey(String messageToEncrypt, byte[] nonSecretPayload = null);
        string DecryptWithKey(string encryptedMessage, int nonSecretPayloadLength = 0);
    }
}
