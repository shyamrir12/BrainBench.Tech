using AdminPanelModels.UserMangment;
using AdminPanelModels;
using LoginModels;

namespace AdminPanelApp.Data.AdminPanelServices.SMSTemplateMasterServices
{
    public interface ISMSTemplateMasterSubscriber
    {
        Task<Result<MessageEF>> AddSMSTemplateMaster(SMSTemplateMaster model);
        Task<Result<List<SMSTemplateMaster>>> ViewSMSTemplateMaster(CommanRequest model);
        Task<Result<SMSTemplateMaster>> EditSMSTemplatemaster(CommanRequest model);
        Task<Result<MessageEF>> DeleteSMSTemplatemaster(CommanRequest model);
        Task<Result<MessageEF>> UpdateSMSTemplatemaster(SMSTemplateMaster model);
    }
}
