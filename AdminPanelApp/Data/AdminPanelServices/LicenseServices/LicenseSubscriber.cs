using AdminPanelModels;
using AdminPanelModels.UserMangment;
using LoginModels;
using System.Net.Http.Json;

namespace AdminPanelApp.Data.AdminPanelServices.LicenseServices
{
    public class LicenseSubscriber : ILicenseSubscriber
    {
        private readonly HttpClient _httpClient;

        public LicenseSubscriber(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Result<MessageEF>> AddUpdateLicense(LicenseModel model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<LicenseModel>($"/License/AddUpdateLicense", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<MessageEF>>();
        }

        public async Task<Result<LicenseModel>> GetLicenseBYID(CommanRequest model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommanRequest>($"/License/GetLicenseBYID", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<LicenseModel>>();
        }

        public async Task<Result<List<LicenseModel>>> GetLicenseList(CommanRequest model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommanRequest>($"/License/GetLicenseList", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<List<LicenseModel>>>();
        }

        public async Task<Result<List<LicenseTranModel>>> GetLicenseTranList(CommanRequest model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommanRequest>($"/License/GetLicenseTranList", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<List<LicenseTranModel>>>();
        }

        public async Task<Result<List<ListItems>>> GetLicenseTypeList(CommanRequest model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommanRequest>($"/License/GetLicenseTypeList", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<List<ListItems>>>();
        }

        public async Task<Result<MessageEF>> ModifyStatusLicense(CommanRequest model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommanRequest>($"/License/ModifyStatusLicense", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<MessageEF>>();
        }

        public async Task<Result<MessageEF>> SubscribeLicense(LicenseTranModel model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<LicenseTranModel>($"/License/SubscribeLicense", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<MessageEF>>();
        }

        public async Task<Result<MessageEF>> UnSubscribeLicense(CommanRequest model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommanRequest>($"/License/UnSubscribeLicense", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<MessageEF>>();
        }

        public async Task<Result<MessageEF>> UpdatePaymentStatus(LicenseTranModel model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<LicenseTranModel>($"/License/UpdatePaymentStatus", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<MessageEF>>();
        }
    }
}
