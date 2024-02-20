using AdminPanelAPI.Areas.AdminPanel.Data.SMSTempateMasterServices;
using AdminPanelModels;
using AdminPanelModels.UserMangment;
using LoginModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanelAPI.Areas.AdminPanel.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
   // [Authorize(Roles = "Admin")]
	public class SMSTemplateController : ControllerBase
	{
		private readonly ISMSTempateMasterProvider _smstempatemasterprovider;

		public SMSTemplateController(ISMSTempateMasterProvider smstempatemasterprovider)
		{
            _smstempatemasterprovider= smstempatemasterprovider;

        }
       
        public Task<Result< MessageEF>> Add(SMSTemplateMaster model)
        {
            return _smstempatemasterprovider.AddSMSTemplateMaster(model);
        }
        public Task<Result<List<SMSTemplateMaster>>> View(CommanRequest model)
        {
            return _smstempatemasterprovider.ViewSMSTemplateMaster(model);
        }
        public Task<Result<SMSTemplateMaster>> Edit(CommanRequest model)
        {
            return _smstempatemasterprovider.EditSMSTemplatemaster(model);
        }
        public Task<Result<MessageEF>> Delete(CommanRequest model)
        {
            return _smstempatemasterprovider.DeleteSMSTemplatemaster(model);
        }
        public Task<Result<MessageEF>> Update(SMSTemplateMaster model)
        {
            return _smstempatemasterprovider.UpdateSMSTemplatemaster(model);
        }

    }
}
