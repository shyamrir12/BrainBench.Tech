using AdminPanelModels;
using LoginModels;

namespace AdminPanelApp.Data.MailSMSServices
{
    public interface IMailSMSSubscriber
    {
       Task< MessageEF> Main(SMS sMS);
        Task<MessageEF> SendCommonMail(CommonMail obj);
    }
}
