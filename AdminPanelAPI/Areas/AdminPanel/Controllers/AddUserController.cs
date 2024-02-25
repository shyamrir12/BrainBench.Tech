
using AdminPanelAPI.Areas.AdminPanel.Data.AddUserServices;
using AdminPanelModels;
using AdminPanelModels.UserMangment;
using LoginModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanelAPI.Areas.AdminPanel.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
	public class AddUserController : ControllerBase
    {
        private readonly IAddUserProvider _objIAddUserProvider;
        public AddUserController(IAddUserProvider objIAddUserProvider)
        {
            _objIAddUserProvider = objIAddUserProvider;
        }
        public Task<Result<MessageEF>> AddUpdateUser(AddUserModel  model)
        {
            return _objIAddUserProvider.AddUpdateUser(model);
        }
        public Task<Result<MessageEF>> ActivationUser(CommanRequest model)
        {
            return _objIAddUserProvider.ActivationUser(model);
        }
        public Task<Result<List<AddUserModel>>> GetUserList(CommanRequest model)
        {
            return _objIAddUserProvider.GetUserList(model);
        }
        public Task<Result<List<ListItems>>> GetRole(CommanRequest model)
        {
            return _objIAddUserProvider.GetRole(model);
        }
        public Task<Result<AddUserModel>> GetUserByID(CommanRequest model)
        {
            return _objIAddUserProvider.GetUserByID(model);
        }

        public Task<Result<MessageEF>> UpdatePassword(CommanRequest model)
        {
            return _objIAddUserProvider.UpdatePassword(model);
        }

    }
}
