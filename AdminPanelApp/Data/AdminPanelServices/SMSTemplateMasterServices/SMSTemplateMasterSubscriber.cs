using AdminPanelModels;
using AdminPanelModels.UserMangment;
using LoginModels;
using System.Net.Http.Json;

namespace AdminPanelApp.Data.AdminPanelServices.SMSTemplateMasterServices
{
    public class SMSTemplateMasterSubscriber : ISMSTemplateMasterSubscriber
    {
        private readonly HttpClient _httpClient;

        public SMSTemplateMasterSubscriber(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Result<MessageEF>> AddSMSTemplateMaster(SMSTemplateMaster model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<SMSTemplateMaster>($"/SMSTemplate/Add", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<MessageEF>>();
        }

        public async Task<Result<MessageEF>> DeleteSMSTemplatemaster(CommanRequest model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommanRequest>($"/SMSTemplate/Delete", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<MessageEF>>();
        }

        public async Task<Result<SMSTemplateMaster>> EditSMSTemplatemaster(CommanRequest model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommanRequest>($"/SMSTemplate/Edit", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<SMSTemplateMaster>>();
        }

        public async Task<Result<MessageEF>> UpdateSMSTemplatemaster(SMSTemplateMaster model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<SMSTemplateMaster>($"/SMSTemplate/Update", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<MessageEF>>();
        }

        public async Task<Result<List<SMSTemplateMaster>>> ViewSMSTemplateMaster(CommanRequest model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommanRequest>($"/SMSTemplate/View", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<List< SMSTemplateMaster >>> ();
        }
    }
}
