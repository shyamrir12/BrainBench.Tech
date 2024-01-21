using IntegrationModels;
using LoginModels;
using Microsoft.AspNetCore.Connections;

namespace IntegrationApi.Data.AzureHelperServices
{
	public class AzureHelperProvider :  IAzureHelperProvider
	{
		private readonly IConfiguration configuration;
		public AzureHelperProvider(IConfiguration _configuration) 
		{
			configuration = _configuration;
		}

		public Task<MessageEF> CheckFileExistance(MyFileRequest FileNameWithPath)
		{
			AzureFileHelper fl = new AzureFileHelper(configuration);
			return fl.CheckFileAlreadyExistsFromAzure(FileNameWithPath.FileNameWithPath);
		}

		public Task<MessageEF> DeleteFile(MyFileRequest FileNameWithPath)
		{
			AzureFileHelper fl = new AzureFileHelper(configuration);
			return fl.DeleteFileFromAzure(FileNameWithPath.FileNameWithPath);
		}

		public Task<MyFileResult> DownloadFile(MyFileRequest FileNameWithPath)
		{
			if (configuration["AzurSettings:UseLocal"] == "Yes")
			{
				LocalFileHelper fl = new LocalFileHelper(configuration);
				return fl.DownloadFileFromLocal(FileNameWithPath.FileNameWithPath);
			}
			else
			{
				AzureFileHelper fl = new AzureFileHelper(configuration);
				return fl.DownloadFileFromAzure(FileNameWithPath.FileNameWithPath);
			}

		}

		public async Task<MessageEF> SaveFile(MyFileRequest objParams)
		{
			if (configuration["AzurSettings:UseLocal"] == "Yes")
			{
				LocalFileHelper fl = new LocalFileHelper(configuration);
				return await fl.SaveFileToLocaBase64(objParams);
			}
			else
			{
				AzureFileHelper fl = new AzureFileHelper(configuration);
				return await fl.SaveFileToAzureBase64(objParams);
			}

		}

		public MessageEF SaveFiles(MyFileRequest FileNameWithPath)
		{
			if (configuration["AzurSettings:UseLocal"] == "Yes")
			{
				LocalFileHelper fl = new LocalFileHelper(configuration);
				return fl.SaveFileToLocaBase64s(FileNameWithPath);
			}
			else
			{
				AzureFileHelper fl = new AzureFileHelper(configuration);
				return fl.SaveFileToAzureBase64s(FileNameWithPath);
			}
		}
	}
}
