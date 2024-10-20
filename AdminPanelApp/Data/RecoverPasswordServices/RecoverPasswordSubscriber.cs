using AdminPanelModels;
using LoginModels;
using System.Net.Http.Json;

namespace AdminPanelApp.Data.RecoverPasswordServices
{
    public class RecoverPasswordSubscriber : IRecoverPasswordSubscriber
    {
        private readonly HttpClient _httpClient;

        public RecoverPasswordSubscriber(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Result<MessageEF>> GetRecoverPassword(RecoverPassword model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<RecoverPassword>($"/RecoverPassword/GetRecoverPassword", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<MessageEF>>();
        }
    }
}
