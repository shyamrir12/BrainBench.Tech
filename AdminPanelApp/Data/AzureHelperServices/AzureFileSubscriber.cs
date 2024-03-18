using AdminPanelModels;
using LoginModels;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace AdminPanelApp.Data.AzureHelperServices
{
    public class AzureFileSubscriber : IAzureFileSubscriber
    {
        private HttpClient _httpClient;

        public AzureFileSubscriber(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Task<MessageEF> CheckFileExistance(MyFileRequest FileNameWithPath)
        {
            throw new NotImplementedException();
        }

        public async Task<MessageEF> DeleteFile(MyFileRequest filerequest)
        {
            
           try
            {

                var httpMessageReponse = await _httpClient.PostAsJsonAsync<MyFileRequest>($"/AzureFile/DeleteFile", filerequest);

                return await httpMessageReponse.Content.ReadFromJsonAsync<MessageEF>();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task< MyFileResult> DownloadFile( MyFileRequest filerequest)
        {
            try
            {
              
                var httpMessageReponse = await _httpClient.PostAsJsonAsync<MyFileRequest>($"/AzureFile/DownloadFileResult", filerequest);

                return await httpMessageReponse.Content.ReadFromJsonAsync<MyFileResult>();

    
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<MessageEF> SaveFile(  MyFileRequest objParams)
        {

            try
            {

                var httpMessageReponse = await _httpClient.PostAsJsonAsync<MyFileRequest>($"/AzureFile/SaveFile", objParams);

                return await httpMessageReponse.Content.ReadFromJsonAsync<MessageEF>();


            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

       
    }
}
