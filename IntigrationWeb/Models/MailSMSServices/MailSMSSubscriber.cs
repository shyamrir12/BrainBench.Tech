using IntegrationModels;
using IntigrationWeb.Models.Utility;
using LoginModels;
using Newtonsoft.Json;

namespace IntigrationWeb.Models.MailSMSServices
{
    public class MailSMSSubscriber : IMailSMSSubscriber
    {
        private readonly IHttpWebClients _objIHttpWebClients;

        public MailSMSSubscriber(IHttpWebClients objIHttpWebClients)
        {
            _objIHttpWebClients = objIHttpWebClients;
        }
        public async Task<MessageEF> Main(SMS sMS)
        {
            MessageEF httpMessageReponse = new MessageEF();
            try
            {
                httpMessageReponse = JsonConvert.DeserializeObject<MessageEF>(await _objIHttpWebClients.AwaitPostRequest("/SMSService/Main", JsonConvert.SerializeObject(sMS)));
                return httpMessageReponse;
            }
            catch (Exception ex)
            {

                throw ex;
            }


           
        }

        public async Task<MessageEF> SendCommonMail(CommonMail obj)
        {
            MessageEF httpMessageReponse = new MessageEF();
            try
            {
                httpMessageReponse = JsonConvert.DeserializeObject<MessageEF>(await _objIHttpWebClients.AwaitPostRequest("/MailService/SendCommonMail", JsonConvert.SerializeObject(obj)));
                return httpMessageReponse;
            }
            catch (Exception ex)
            {

                throw ex;
            }
  
        }
    }
}
