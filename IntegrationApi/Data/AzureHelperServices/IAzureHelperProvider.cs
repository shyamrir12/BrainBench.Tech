using IntegrationModels;
using LoginModels;

namespace IntegrationApi.Data.AzureHelperServices
{
	public interface IAzureHelperProvider
	{
		Task<MessageEF> SaveFile(IntegrationModels.MyFileRequest objParams);
		Task<MyFileResult> DownloadFile(MyFileRequest FileNameWithPath);
		Task<MessageEF> DeleteFile(MyFileRequest FileNameWithPath);
		Task<MessageEF> CheckFileExistance(MyFileRequest FileNameWithPath);
		MessageEF SaveFiles(MyFileRequest FileNameWithPath);
	}
}
