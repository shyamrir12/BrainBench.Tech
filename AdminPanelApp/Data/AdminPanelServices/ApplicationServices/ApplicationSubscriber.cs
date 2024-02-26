using AdminPanelModels;
using AdminPanelModels.UserMangment;
using LoginModels;
using System.Net.Http.Json;

namespace AdminPanelApp.Data.AdminPanelServices.ApplicationServices
{
    public class ApplicationSubscriber : IApplicationSubscriber
    {
        private readonly HttpClient _httpClient;

        public ApplicationSubscriber(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Result<MessageEF>> AddApplication(DmsIssuedBy model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<DmsIssuedBy>($"/Application/AddApplication", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<MessageEF>>();
        }

        public async Task<Result<DmsIssuedBy>> GetApplicationBYID(CommanRequest model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommanRequest>($"/Application/GetApplicationBYID", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<DmsIssuedBy>>();
        }

        public async Task<Result<List<DmsIssuedBy>>> GetApplicationList(CommanRequest model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommanRequest>($"/Application/GetApplicationList", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<List<DmsIssuedBy>>>();
        }

        public async Task<Result<MessageEF>> ModifyStatusApplication(CommanRequest model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommanRequest>($"/Application/ModifyStatusApplication", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<MessageEF>>();
        }

        public async Task<Result<MessageEF>> UpdateApplication(DmsIssuedBy model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<DmsIssuedBy>($"/Application/UpdateApplication", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<MessageEF>>();
        }
    }
}
