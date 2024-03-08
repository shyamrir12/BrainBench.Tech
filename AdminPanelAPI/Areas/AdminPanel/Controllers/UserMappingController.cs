using AdminPanelAPI.Areas.AdminPanel.Data.UserMappingServices;
using AdminPanelModels;
using LoginModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanelAPI.Areas.AdminPanel.Controllers
{
    [Route("api/{controller}/{action}")]
    [ApiController]
    [Authorize]
    public class UserMappingController : ControllerBase
	{
		private readonly IUserMappingProvider _userMappingProvider;
		public UserMappingController(IUserMappingProvider userMappingProvider)
		{
			_userMappingProvider = userMappingProvider;
		}
        public Task<Result<List<ListItems>>> GetApplication(CommanRequest model)
        {
            return _userMappingProvider.GetApplication(model);
        }
        public Task<Result<List<ListItems>>> GetWorkspace(CommanRequest model)
        {
            return _userMappingProvider.GetWorkspace(model);
        }
        public Task<Result<List<ListItems>>> GetOutlet(CommanRequest model)
        {
            return _userMappingProvider.GetOutlet(model);
        }
        public Task<Result<MessageEF>> AssignApplication(CommanRequest model)
        {
            return _userMappingProvider.AssignApplication(model);
        }
        public Task<Result<MessageEF>> AssignWorkspace(CommanRequest model)
        {
            return _userMappingProvider.AssignWorkspace(model);
        }
        public Task<Result<MessageEF>> AssignOutlet(CommanRequest model)
        {
            return _userMappingProvider.AssignOutlet(model);
        }
        //user sub branch mapping
    }
}
