using AdminPanelModels;
using AdminPanelModels.UserMangment;
using LoginModels;
using System.Net.Http.Json;

namespace AdminPanelApp.Data.AdminPanelServices.AddUserServices
{
    public class AddUserSubscriber : IAddUserSubscriber
    {
        private readonly HttpClient _httpClient;

        public AddUserSubscriber(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Result<MessageEF>> ActivationUser(CommanRequest model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommanRequest>($"/AddUser/ActivationUser", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<MessageEF>>();
        }

        public async Task<Result<MessageEF>> AddUpdateUser(AddUserModel model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<AddUserModel>($"/AddUser/AddUpdateUser", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<MessageEF>>();
        }

        public async Task<Result<List<ListItems>>> GetRole(CommanRequest model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommanRequest>($"/AddUser/GetRole", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<List<ListItems>>>();
        }

        public async Task<Result<AddUserModel>> GetUserByID(CommanRequest model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommanRequest>($"/AddUser/GetUserByID", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<AddUserModel>>();
        }

        public async Task<Result<List<AddUserModel>>> GetUserList(CommanRequest model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommanRequest>($"/AddUser/GetUserList", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<List<AddUserModel>>>();
        }

        public async Task<Result<MessageEF>> UpdatePassword(CommanRequest model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommanRequest>($"/AddUser/UpdatePassword", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<MessageEF>>();
        }
    }
}
