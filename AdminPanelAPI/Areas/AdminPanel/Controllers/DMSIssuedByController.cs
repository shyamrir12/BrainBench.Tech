using AdminPanelAPI.Areas.AdminPanel.Data.DmsIssuedByServices;
using AdminPanelModels;
using AdminPanelModels.UserMangment;
using LoginModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanelAPI.Areas.AdminPanel.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DMSIssuedByController : ControllerBase
	{
		private readonly IDmsIssuedByProvider _dmsIssuedByProvider;

        public DMSIssuedByController(IDmsIssuedByProvider dmsIssuedByProvider)
        {
            _dmsIssuedByProvider = dmsIssuedByProvider;
        }

        public Task<Result<MessageEF>> AddApplication(DmsIssuedBy model)
        {
            return _dmsIssuedByProvider.AddApplication(model);
        }
        public Task<Result<MessageEF>> UpdateApplication(DmsIssuedBy model)
        {
            return _dmsIssuedByProvider.UpdateApplication(model);
        }
        public Task<Result<MessageEF>> ModifyStatusApplication(CommanRequest model)
        {
            return _dmsIssuedByProvider.ModifyStatusApplication(model);
        }
        public Task<Result<DmsIssuedBy>> GetApplicationBYID(CommanRequest model)
        {
            return _dmsIssuedByProvider.GetApplicationBYID(model);
        }
        public Task<Result<List<DmsIssuedBy>>> GetApplicationList(CommanRequest model)
        {
            return _dmsIssuedByProvider.GetApplicationList(model);
        }
        //Application Type-Medical/restarent
    }
}
