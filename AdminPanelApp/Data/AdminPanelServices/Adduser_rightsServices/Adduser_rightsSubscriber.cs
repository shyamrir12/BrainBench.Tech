using AdminPanelModels;
using LoginModels;
using System.Net.Http.Json;

namespace AdminPanelApp.Data.AdminPanelServices.Adduser_rightsServices
{
    public class Adduser_rightsSubscriber : IAdduser_rightsSubscriber
    {
        private readonly HttpClient _httpClient;

        public Adduser_rightsSubscriber(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Result<List<MenuItem>>> GetMenuListByRole(CommanRequest model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommanRequest>($"/Adduser_rights/GetMenuListByRole", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<List<MenuItem>>>();
        }

        public async Task<Result<List<MenuItem>>> GetMenuListFormate(CommanRequest model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommanRequest>($"/Adduser_rights/GetMenuListFormate", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<List<MenuItem>>>();
        }

        public async Task<Result<List<ListItems>>> GetUserList(CommanRequest model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommanRequest>($"/Adduser_rights/GetUserList", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<List<ListItems>>>();
        }

        public async Task<Result<MessageEF>> UpdateMenuByID(CommanRequest model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommanRequest>($"/Adduser_rights/UpdateMenuByID", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<MessageEF>>();
        }
    }
}
