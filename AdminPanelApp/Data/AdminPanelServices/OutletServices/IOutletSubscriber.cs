using AdminPanelModels.UserMangment;
using AdminPanelModels;
using LoginModels;

namespace AdminPanelApp.Data.AdminPanelServices.OutletServices
{
    public interface IOutletSubscriber
    {
        Task<Result<List<DmsHECategory>>> GetOutletList(CommanRequest model);
        Task<Result<DmsHECategory>> GetOutletBYID(CommanRequest model);
        Task<Result<MessageEF>> AddOutlet(DmsHECategory model);
        Task<Result<MessageEF>> UpdateOutlet(DmsHECategory model);
        Task<Result<MessageEF>> ModifyStatusOutlet(CommanRequest model);
    }
}
