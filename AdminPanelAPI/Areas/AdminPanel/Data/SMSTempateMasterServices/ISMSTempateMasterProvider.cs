using AdminPanelModels;
using AdminPanelModels.UserMangment;
using LoginModels;

namespace AdminPanelAPI.Areas.AdminPanel.Data.SMSTempateMasterServices
{
	public interface ISMSTempateMasterProvider
	{
	Result<MessageEF> AddSMSTemplateMaster(SMSTemplateMaster objStatemaster);
		Result<List<SMSTemplateMaster>> ViewSMSTemplateMaster(SMSTemplateMaster objStatemaster);
		Result<SMSTemplateMaster> EditSMSTemplatemaster(SMSTemplateMaster objStatemaster);
		Result<MessageEF> DeleteSMSTemplatemaster(SMSTemplateMaster objStatemaster);
		Result<MessageEF> UpdateSMSTemplatemaster(SMSTemplateMaster objStatemaster);
	}
}
