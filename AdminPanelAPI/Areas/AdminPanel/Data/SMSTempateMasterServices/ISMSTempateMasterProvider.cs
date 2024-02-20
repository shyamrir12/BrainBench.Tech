using AdminPanelModels;
using AdminPanelModels.UserMangment;
using LoginModels;

namespace AdminPanelAPI.Areas.AdminPanel.Data.SMSTempateMasterServices
{
	public interface ISMSTempateMasterProvider
	{
        Task< Result<MessageEF>> AddSMSTemplateMaster(SMSTemplateMaster model);
        Task<Result<List<SMSTemplateMaster>>> ViewSMSTemplateMaster(CommanRequest model);
		 Task<Result<SMSTemplateMaster>> EditSMSTemplatemaster(CommanRequest model);
		 Task<Result<MessageEF>> DeleteSMSTemplatemaster(CommanRequest model);
		 Task<Result<MessageEF>> UpdateSMSTemplatemaster(SMSTemplateMaster model);
	}
}
