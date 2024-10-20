using AdminPanelModels;
using LoginModels;

namespace AdminPanelApp.Data.AzureHelperServices
{
    public interface IAzureFileSubscriber
    {
        Task<MessageEF> SaveFile( MyFileRequest objParams);
        Task<MyFileResult> DownloadFile( MyFileRequest filerequest);
        Task<MessageEF> DeleteFile(MyFileRequest FileNameWithPath);
        Task<MessageEF> CheckFileExistance(MyFileRequest FileNameWithPath);
    }
}
