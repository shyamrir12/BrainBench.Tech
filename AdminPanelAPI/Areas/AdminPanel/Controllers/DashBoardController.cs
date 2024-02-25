using AdminPanelAPI.Areas.AdminPanel.Data.DashBoardServices;
using AdminPanelModels;
using AdminPanelModels.UserMangment;
using LoginModels;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanelAPI.Areas.AdminPanel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IDashBoardProvider _dashBoardProvider;
        public DashBoardController(IDashBoardProvider dashBoardProvider)
        {
            _dashBoardProvider = dashBoardProvider;
        }
        public Task<Result<DashboardModel>> GetDashboard(CommanRequest model)
        {
            return _dashBoardProvider.GetDashboard(model);
        }

    }
}
