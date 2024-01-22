using AdminPanelAPI.Data.RegisterServices;
using AdminPanelModels;
using LoginModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanelAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [Route("api/[controller]")]
	[ApiController]
	public class RegisterController : ControllerBase
	{
        private readonly IRegisterProvider _objIRegisterProvider;

        public RegisterController(IRegisterProvider objIRegisterProvider)
        {
            _objIRegisterProvider = objIRegisterProvider;
        }
        public Task<Result<MessageEF>> CheckUserExist(RegisterUser model)
        {
            return _objIRegisterProvider.CheckUserExist(model);
        }
        public Task<Result<MessageEF>> RegisterUser(RegisterUser model)
        {
            return _objIRegisterProvider.RegisterUser(model);
        }
        public Task<Result<List<ListItems>>> GetApplicationType()
        {
            return _objIRegisterProvider.GetApplicationType();
        }
    }
}
