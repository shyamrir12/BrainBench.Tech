using AdminPanelModels;
using AdminPanelModels.UserMangment;
using LoginModels;
using System.Net.Http.Json;

namespace AdminPanelApp.Data.AdminPanelServices.OutletServices
{
    public class OutletSubscriber : IOutletSubscriber
    {
        private readonly HttpClient _httpClient;

        public OutletSubscriber(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Result<MessageEF>> AddOutlet(DmsHECategory model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<DmsHECategory>($"/Outlet/AddOutlet", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<MessageEF>>();
        }

        public async Task<Result<DmsHECategory>> GetOutletBYID(CommanRequest model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommanRequest>($"/Outlet/GetOutletBYID", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<DmsHECategory>>();
        }

        public async Task<Result<List<DmsHECategory>>> GetOutletList(CommanRequest model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommanRequest>($"/Outlet/GetOutletList", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<List<DmsHECategory>>>();
        }

        public async Task<Result<MessageEF>> ModifyStatusOutlet(CommanRequest model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommanRequest>($"/Outlet/ModifyStatusOutlet", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<MessageEF>>();
        }

        public async Task<Result<MessageEF>> UpdateOutlet(DmsHECategory model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<DmsHECategory>($"/Outlet/UpdateOutlet", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<MessageEF>>();
        }
    }
}
