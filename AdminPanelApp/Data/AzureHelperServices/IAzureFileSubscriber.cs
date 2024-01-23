using AdminPanelModels;
using LoginModels;

namespace AdminPanelApp.Data.AzureHelperServices
{
    public interface IAzureFileSubscriber
    {
        Task<MessageEF> SaveFile(string IntergationHost, MyFileRequest objParams);
        Task<MyFileResult> DownloadFile(string IntergationHost, MyFileRequest filerequest);
        Task<MessageEF> DeleteFile(MyFileRequest FileNameWithPath);
        Task<MessageEF> CheckFileExistance(MyFileRequest FileNameWithPath);
    }
}
