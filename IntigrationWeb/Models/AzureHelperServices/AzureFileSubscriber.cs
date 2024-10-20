using IntegrationModels;
using IntigrationWeb.Models.Utility;
using LoginModels;
using Newtonsoft.Json;

namespace IntigrationWeb.Models.AzureHelperServices
{
    public class AzureFileSubscriber : IAzureFileSubscriber
    {
       private readonly IHttpWebClients _objIHttpWebClients;
        public AzureFileSubscriber(IHttpWebClients objIHttpWebClients)
        {
            _objIHttpWebClients = objIHttpWebClients;
        }
        public Task<MessageEF> CheckFileExistance(MyFileRequest FileNameWithPath)
        {
            throw new NotImplementedException();
        }

        public Task<MessageEF> DeleteFile(MyFileRequest FileNameWithPath)
        {
            throw new NotImplementedException();
        }

        public Task<MyFileResult> DownloadFile(MyFileRequest filerequest)
        {
            try
            {
             
                return JsonConvert.DeserializeObject<Task<MyFileResult>>(_objIHttpWebClients.PostRequest("AzureFile/DownloadFileResult", JsonConvert.SerializeObject(filerequest)));

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Task<MessageEF> SaveFile(MyFileRequest objParams)
        {
            try
            {
                return JsonConvert.DeserializeObject<Task<MessageEF>>(_objIHttpWebClients.PostRequest("AzureFile/SaveFile", JsonConvert.SerializeObject(objParams)));

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
