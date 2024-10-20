using AdminPanelModels;
using LoginModels;
using System.Net.Http.Json;

namespace AdminPanelApp.Data.MailSMSServices
{
    public class MailSMSSubscriber : IMailSMSSubscriber
    {
        private HttpClient _httpClient;

        public MailSMSSubscriber(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<MessageEF> Main(SMS sMS)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<SMS>($"/SMSService/Main", sMS);

            return await httpMessageReponse.Content.ReadFromJsonAsync<MessageEF>();
        }

        public async Task<MessageEF> SendCommonMail(CommonMail obj)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommonMail>($"/MailService/SendCommonMail", obj);

            return await httpMessageReponse.Content.ReadFromJsonAsync<MessageEF>();
        }
    }
}
