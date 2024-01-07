using IntegrationModels;

namespace IntegrationApi.Data.EncryptionServices
{
	public interface IEncryptionProvider
	{
		EncryptionModel GetEncryptVal(EncryptionModel EncOutput);
		EncryptionModel GetDecryptVal(EncryptionModel EncOutput);
	}
}
