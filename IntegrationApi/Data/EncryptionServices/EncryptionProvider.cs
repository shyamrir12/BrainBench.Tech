using CSMEncryption;
using IntegrationApi.Factory;
using IntegrationModels;

namespace IntegrationApi.Data.EncryptionServices
{
	public class EncryptionProvider: IEncryptionProvider
	{
		
		public EncryptionModel GetDecryptVal(EncryptionModel EncOutput)
		{
			return new EncryptionModel() { ResponseData = GetDecryptValI(EncOutput.RequestData) };
		}

		public EncryptionModel GetEncryptVal(EncryptionModel EncOutput)
		{
			return new EncryptionModel() { ResponseData = EncryptValI(EncOutput.RequestData) };
		}

		public string GetDecryptValI(string EncOutput)
		{
			string passPhrase = null;
			string saltValue = null;
			string hashAlgorithm = null;
			int passwordIterations = 0;
			string initVector = null;
			int keySize = 0;
			string DecryptOutput = "";

			try
			{
				passPhrase = "Pas5pr@se";
				// can be any string
				saltValue = "s@1tValue";
				// can be any string
				hashAlgorithm = "SHA1";
				// can be "MD5"
				passwordIterations = 2;
				// can be any number
				initVector = "@1B2c3D4e5F6g7H8";
				// must be 16 bytes
				keySize = 256;
				// can be 192 or 128


				DecryptOutput = XMLEncrypt.Decrypt(EncOutput, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				throw;
			}

			return DecryptOutput;
		}

		private string EncryptValI(string ePassNo)
		{
			string passPhrase = null;
			string saltValue = null;
			string hashAlgorithm = null;
			int passwordIterations = 0;
			string initVector = null;
			int keySize = 0;
			string EncOutput = "";

			try
			{
				passPhrase = "Pas5pr@se";
				// can be any string
				saltValue = "s@1tValue";
				// can be any string
				hashAlgorithm = "SHA1";
				// can be "MD5"
				passwordIterations = 2;
				// can be any number
				initVector = "@1B2c3D4e5F6g7H8";
				// must be 16 bytes
				keySize = 256;
				// can be 192 or 128

				EncOutput = XMLEncrypt.Encrypt(ePassNo, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
			}
			catch (Exception ex)
			{
				return "";
			}
			return EncOutput;
		}
	}
}
