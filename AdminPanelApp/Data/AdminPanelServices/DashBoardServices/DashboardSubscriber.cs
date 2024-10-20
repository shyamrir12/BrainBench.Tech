using AdminPanelModels;
using AdminPanelModels.UserMangment;
using LoginModels;
using System.Net.Http.Json;

namespace AdminPanelApp.Data.AdminPanelServices.DashBoardServices
{
    public class DashboardSubscriber : IDashboardSubscriber
    {
        private readonly HttpClient _httpClient;

        public DashboardSubscriber(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Result<DashboardModel>> GetDashboard(CommanRequest model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommanRequest>($"/Dashboard/GetDashboard", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<DashboardModel>>();
        }
    }
}
