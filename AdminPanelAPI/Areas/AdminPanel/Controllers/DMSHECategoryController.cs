using AdminPanelAPI.Areas.AdminPanel.Data.DepartmentServices;
using AdminPanelAPI.Areas.AdminPanel.Data.DmsHECategoryServices;
using AdminPanelModels.UserMangment;
using AdminPanelModels;
using LoginModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanelAPI.Areas.AdminPanel.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DMSHECategoryController : ControllerBase
    {
        private readonly IDmsHECategoryProvider  _dmsHECategoryProvider;
        public Task<Result<MessageEF>> AddOutlet(DmsHECategory model)
        {
            return _dmsHECategoryProvider.AddOutlet(model);
        }
        public Task<Result<MessageEF>> UpdateOutlet(DmsHECategory model)
        {
            return _dmsHECategoryProvider.UpdateOutlet(model);
        }
        public Task<Result<MessageEF>> ModifyStatusOutlet(CommanRequest model)
        {
            return _dmsHECategoryProvider.ModifyStatusOutlet(model);
        }
        public Task<Result<DmsHECategory>> GetOutletBYID(CommanRequest model)
        {
            return _dmsHECategoryProvider.GetOutletBYID(model);
        }
        public Task<Result<List<DmsHECategory>>> GetOutletList(CommanRequest model)
        {
            return _dmsHECategoryProvider.GetOutletList(model);
        }
        //Application Onner-Main Branch
    }
}
