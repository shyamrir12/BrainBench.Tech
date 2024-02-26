using AdminPanelModels.UserMangment;
using AdminPanelModels;
using LoginModels;

namespace AdminPanelApp.Data.AdminPanelServices.DashBoardServices
{
    public interface IDashboardSubscriber
    {
        Task<Result<DashboardModel>> GetDashboard(CommanRequest model);
    }
}
