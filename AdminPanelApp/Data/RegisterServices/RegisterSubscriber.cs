using AdminPanelModels;
using LoginModels;
using System.Net.Http.Json;

namespace AdminPanelApp.Data.RegisterServices
{
    public class RegisterSubscriber : IRegisterSubscriber
    {
        private HttpClient _httpClient;

        public RegisterSubscriber(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Result<MessageEF>> CheckUserExist(RegisterUser model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<RegisterUser>($"/Register/CheckUserExist", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<MessageEF>>();
        }

        public async Task<Result<List<ListItems>>> GetApplicationType()
        {
           
            var httpMessageReponse = await _httpClient.PostAsync($"/Register/GetApplicationType", null);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<List<ListItems>>>();
        }

        public async Task<Result<MessageEF>> RegisterUser(RegisterUser model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<RegisterUser>($"/Register/RegisterUser", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<MessageEF>>();
        }
    }
}
