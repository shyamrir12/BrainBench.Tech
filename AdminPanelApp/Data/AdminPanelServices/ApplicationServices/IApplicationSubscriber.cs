using AdminPanelModels.UserMangment;
using AdminPanelModels;
using LoginModels;

namespace AdminPanelApp.Data.AdminPanelServices.ApplicationServices
{
    public interface IApplicationSubscriber
    {
        Task<Result<List<DmsIssuedBy>>> GetApplicationList(CommanRequest model);
        Task<Result<DmsIssuedBy>> GetApplicationBYID(CommanRequest model);
        Task<Result<MessageEF>> AddApplication(DmsIssuedBy model);
        Task<Result<MessageEF>> UpdateApplication(DmsIssuedBy model);
        Task<Result<MessageEF>> ModifyStatusApplication(CommanRequest model);
    }
}
