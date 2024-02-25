using AdminPanelModels.UserMangment;
using AdminPanelModels;
using LoginModels;

namespace AdminPanelAPI.Areas.AdminPanel.Data.DashBoardServices
{
	public interface IDashBoardProvider
	{
        Task<Result<DashboardModel>> GetDashboard(CommanRequest model);
    }
}
