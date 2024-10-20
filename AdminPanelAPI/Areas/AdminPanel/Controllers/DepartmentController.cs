using AdminPanelAPI.Areas.AdminPanel.Data.DepartmentServices;
using AdminPanelModels.UserMangment;
using AdminPanelModels;
using LoginModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdminPanelAPI.Areas.AdminPanel.Data.UserMappingServices;

namespace AdminPanelAPI.Areas.AdminPanel.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DepartmentController : ControllerBase
	{
        private readonly IDepartmentProvider _departmentProvider;
        private readonly IUserMappingProvider _userMappingProvider;

        public DepartmentController(IDepartmentProvider departmentProvider, IUserMappingProvider userMappingProvider)
        {
            _departmentProvider = departmentProvider;
            _userMappingProvider = userMappingProvider;
        }

        public Task<Result<List<ListItems>>> GetApplication(CommanRequest model)
        {
            return _userMappingProvider.GetApplication(model);
        }
        public Task<Result<MessageEF>> AddWorkspace(Department model)
        {
            return _departmentProvider.AddWorkspace(model);
        }
        public Task<Result<MessageEF>> UpdateWorkspace(Department model)
        {
            return _departmentProvider.UpdateWorkspace(model);
        }
        public Task<Result<MessageEF>> ModifyStatusWorkspace(CommanRequest model)
        {
            return _departmentProvider.ModifyStatusWorkspace(model);
        }
        public Task<Result<Department>> GetWorkspaceBYID(CommanRequest model)
        {
            return _departmentProvider.GetWorkspaceBYID(model);
        }
        public Task<Result<List<Department>>> GetWorkspaceList(CommanRequest model)
        {
            return _departmentProvider.GetWorkspaceList(model);
        }
        //Application sub user-Sub Branch
    }
}
