using IntegrationModels;
using LoginModels;

namespace IntigrationWeb.Models.MailSMSServices
{
    public interface IMailSMSSubscriber
    {
        Task<MessageEF> Main(SMS sMS);
        Task<MessageEF> SendCommonMail(CommonMail obj);
    }
}
